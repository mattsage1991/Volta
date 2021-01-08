using System;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Volta.BuildingBlocks.Infrastructure;
using Volta.Stocks.Infrastructure;

namespace Volta.Stocks.Integration.Core.Setup
{
    public class TestInfrastructureAutofacModule : Autofac.Module
    {
        private readonly string databaseConnectionString;
        private readonly bool useSqlServer;
        private readonly bool useSqlite;

        public TestInfrastructureAutofacModule(string databaseConnectionString, bool useSqlServer = false, bool useSqlite = false)
        {
            this.databaseConnectionString = databaseConnectionString ?? throw new ArgumentNullException(nameof(databaseConnectionString)); ;
            this.useSqlServer = useSqlServer;
            this.useSqlite = useSqlite;
        }

        /// <summary>
        /// Infrastructure Module to add overrides for use with integration tests.
        /// </summary>
        /// <param name="builder">The builder <see cref="ContainerBuilder"/>.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var opt = new DbContextOptionsBuilder<StocksContext>()
                    .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

                if (this.useSqlServer)
                {
                    opt.UseSqlServer(this.databaseConnectionString, sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
                }
                else if (this.useSqlite)
                {
                    opt.UseSqlite(this.databaseConnectionString);
                }

                return new StocksContext(opt.Options);
            }).AsSelf().InstancePerLifetimeScope();
        }
    }
}