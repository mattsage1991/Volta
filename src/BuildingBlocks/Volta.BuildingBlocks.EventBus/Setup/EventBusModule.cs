using Autofac;
using Volta.BuildingBlocks.EventBus.IntegrationEventLog.Services;

namespace Volta.BuildingBlocks.EventBus.Setup
{
    public class EventBusModule : Module
    {
        public EventBusModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InternalIntegrationEventPublisher>().AsImplementedInterfaces().InstancePerDependency();

            base.Load(builder);
        }
    }
}
