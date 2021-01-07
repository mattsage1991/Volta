using Autofac;
using Microsoft.EntityFrameworkCore;
using Volta.BuildingBlocks.Application.EventBus;
using Volta.BuildingBlocks.Infrastructure.DomainEventsAccessing;
using Volta.BuildingBlocks.Infrastructure.DomainEventsDispatching;
using Volta.BuildingBlocks.Infrastructure.IntegrationEventLog;

namespace Volta.BuildingBlocks.Infrastructure.Setup
{
    public class InfrastructureBaseAutofacModule<TDbContext> : Module
        where TDbContext : DbContext
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DomainEventsDispatcher>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<DomainEventsAccessor<TDbContext>>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<IntegrationEventService<TDbContext>>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<IntegrationEventLogService>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<UnitOfWork<TDbContext>>().AsImplementedInterfaces().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}