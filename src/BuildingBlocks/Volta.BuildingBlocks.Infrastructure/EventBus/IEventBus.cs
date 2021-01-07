using Volta.BuildingBlocks.Application.EventBus;

namespace Volta.BuildingBlocks.Infrastructure.EventBus
{
    public interface IEventBus
    {
        void Publish(IntegrationEvent @event);
    }
}