using System.Diagnostics.CodeAnalysis;
using Autofac;
using Volta.BuildingBlocks.EventBus.Setup;
using Volta.BuildingBlocks.Infrastructure.DomainEventsDispatching;

namespace Volta.BuildingBlocks.Infrastructure.Setup
{
    [ExcludeFromCodeCoverage]
    public class InfrastructureAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new EventBusModule());
            builder.RegisterType<DomainEventsDispatcher>().AsImplementedInterfaces().InstancePerDependency();

            base.Load(builder);
        }
    }
}
