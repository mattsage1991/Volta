using System;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Volta.BuildingBlocks.EventBus;
using Volta.BuildingBlocks.EventBus.IntegrationEventLog.Services;
using Volta.BuildingBlocks.Infrastructure.EntityFramework.IntegrationEventServices;
using Volta.BuildingBlocks.Infrastructure.EntityFramework.InternalCommands;
using Volta.BuildingBlocks.Infrastructure.EntityFramework.Quartz;
using Volta.BuildingBlocks.Infrastructure.Setup;

namespace Volta.BuildingBlocks.Infrastructure.EntityFramework.Setup
{

    [ExcludeFromCodeCoverage]
    public class InfrastructureEntityFrameworkAutofacModule<TDbContext> : Autofac.Module
        where TDbContext : DbContext
    {
        private readonly string databaseConnectionString;

        public InfrastructureEntityFrameworkAutofacModule(string databaseConnectionString)
        {
            this.databaseConnectionString = databaseConnectionString ?? throw new ArgumentNullException(nameof(databaseConnectionString));
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new InfrastructureAutofacModule());
            //builder.RegisterModule(new QuartzModule());

            builder.RegisterType<DomainEventsAccessor<TDbContext>>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<UnitOfWork<TDbContext>>().AsImplementedInterfaces().InstancePerLifetimeScope();

            //builder.RegisterType<CurrentTransactionAccessor>().AsImplementedInterfaces().InstancePerLifetimeScope();

            //builder.Register(c =>
            //{
            //    var opt = new DbContextOptionsBuilder<IntegrationEventLogContext>();
            //    opt.UseSqlServer(databaseConnectionString,
            //         sqlServerOptionsAction: sqlOptions =>
            //         {
            //             sqlOptions.MigrationsAssembly(typeof(IntegrationEventLogContext).GetTypeInfo().Assembly.GetName().Name);
            //             //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
            //             sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            //         });
            //    return new IntegrationEventLogContext(opt.Options);
            //}).AsSelf().InstancePerLifetimeScope();

            builder.Register(c =>
            {
                var opt = new DbContextOptionsBuilder<InternalCommandsContext>();
                opt.UseSqlServer(databaseConnectionString,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(InternalCommandsContext).GetTypeInfo().Assembly.GetName().Name);
                        //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
                return new InternalCommandsContext(opt.Options);
            }).AsSelf().InstancePerLifetimeScope();


            //builder.RegisterType<IntegrationEventAccessor>().AsImplementedInterfaces().InstancePerLifetimeScope();
            //builder.RegisterType<IntegrationEventPublisher<TDbContext>>().AsImplementedInterfaces().InstancePerDependency();

            //builder.Register<Func<DbConnection, IIntegrationEventLogService>>(sp =>
            //{
            //    var integrationEventProvider = sp.Resolve<IIntegrationEventTypesProvider>();
            //    return (DbConnection c) => new IntegrationEventLogService(integrationEventProvider, c);
            //});

            base.Load(builder);
        }
    }

}
