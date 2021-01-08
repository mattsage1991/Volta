using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;
using Volta.BuildingBlocks.Infrastructure;
using Volta.Stocks.Integration.Core.Common;
using Volta.Stocks.Integration.Core.Common.Interfaces;
using Volta.Stocks.Integration.Core.Setup;

namespace Volta.Stocks.Integration.Core.Infrastructure.EFCore.SqlServer
{
    public class EFCoreSqlServerTestAdapter<TContext> : ITestAdapter, ITestSetup, ITestContainer, ITestSeeder, ITestChecker, ITestTeardown
        where TContext : DbContext
    {
        private string connectionString;

        protected IServiceCollection _services;
        protected IServiceProvider _provider;

        /// <summary>
        /// Gets the <see cref="TestSeedType"/> seed type.
        /// </summary>
        public TestSeedType SeedType { get; }

        /// <summary>
        /// Gets the <see cref="TestCheckType"/> check type.
        /// </summary>
        public TestCheckType CheckType { get; }

        /// <summary>
        /// Initialises a new instance of the <see cref="EFCoreSqlServerTestAdapter"/> class.
        /// </summary>
        /// <param name="connectionString">A single string value.</param>
        public EFCoreSqlServerTestAdapter(string connectionString)
        {
            this.connectionString = connectionString;
            SeedType = EFCoreTestSeedType.Instance;
            CheckType = EFCoreTestCheckType.Instance;
        }

        public Task SetupAsync(IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();

            // Remove TContext
            services.Remove(services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<TContext>)));
            services.Remove(services.SingleOrDefault(x => x.ServiceType == typeof(TContext)));

            // Add new DbContext
            services.AddDbContext<TContext>(options => options
                .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>()
                .UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                }));

            _services = services;
            _provider = services.BuildServiceProvider();

            return Task.CompletedTask;
        }

        public Task SetupContainerAsync(ContainerBuilder builder)
        {
            builder.RegisterModule(new TestApplicationAutofacModule(connectionString));
            builder.RegisterModule(new TestInfrastructureAutofacModule(connectionString, useSqlServer: true));

            return Task.CompletedTask;
        }

        public async Task SeedAsync(List<object> seedData)
        {
            if (seedData.Any())
            {
                var context = GetSeedContext();
                var typeGroups = seedData.GroupBy(x => x.GetType());

                foreach (var group in typeGroups)
                {
                    var entityType = context.Model.FindEntityType(group.Key);
                    var keyProperties = entityType.FindPrimaryKey().Properties;
                    var tableName = entityType.GetTableName();
                    var entitiesRequiringIdentityInsert = new List<object>();
                    var entitiesNotRequiringIdentityInsert = new List<object>();

                    if (keyProperties.Count == 1)
                    {
                        var keyName = keyProperties[0].Name;
                        var valueGenerated = keyProperties[0].ValueGenerated;
                        var isValueGenerated = valueGenerated == ValueGenerated.OnAdd || valueGenerated == ValueGenerated.OnAddOrUpdate;
                        var isInt = keyProperties[0].ClrType == typeof(int);

                        if (isInt && isValueGenerated)
                        {
                            var valuesAndPks = group.Select(x =>
                            {
                                var pkProperty = x.GetType().GetProperty(keyName);
                                var pkType = pkProperty.GetType();
                                return new
                                {
                                    Value = x,
                                    PrimaryKey = pkProperty.GetValue(x),
                                    PrimaryKeyDefault = pkType.IsValueType ? Activator.CreateInstance(pkType) : null
                                };
                            });

                            entitiesRequiringIdentityInsert = valuesAndPks
                                .Where(x => !x.PrimaryKey.Equals(x.PrimaryKeyDefault))
                                .Select(x => x.Value)
                                .ToList();

                            entitiesNotRequiringIdentityInsert = valuesAndPks
                                .Where(x => x.PrimaryKey.Equals(x.PrimaryKeyDefault))
                                .Select(x => x.Value)
                                .ToList();
                        }
                        else
                        {
                            entitiesNotRequiringIdentityInsert = group.ToList();
                        }
                    }

                    if (entitiesRequiringIdentityInsert.Any())
                    {
                        using (var transaction = await context.Database.BeginTransactionAsync())
                        {
                            var sql = $"SET IDENTITY_INSERT {tableName} ON";
                            await context.Database.ExecuteSqlRawAsync(sql);

                            context.AddRange(entitiesRequiringIdentityInsert);
                            await context.SaveChangesAsync();

                            var sql2 = $"SET IDENTITY_INSERT {tableName} OFF";
                            await context.Database.ExecuteSqlRawAsync(sql2);

                            transaction.Commit();
                        }
                    }

                    context.AddRange(entitiesNotRequiringIdentityInsert);
                    await context.SaveChangesAsync();
                }
            }
        }

        public object GetCheckObject()
        {
            var scope = _provider.CreateScope();
            return scope.ServiceProvider.GetRequiredService<TContext>();
        }

        public Task TeardownAsync()
        {
            return _provider.GetRequiredService<TContext>().Database.EnsureDeletedAsync();
        }

        protected virtual TContext GetSeedContext()
        {
            return _provider.GetRequiredService<TContext>();
        }
    }
}