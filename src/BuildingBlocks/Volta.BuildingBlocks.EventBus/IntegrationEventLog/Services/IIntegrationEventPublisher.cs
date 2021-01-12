using System.Threading.Tasks;
using Volta.BuildingBlocks.EventBus.Events;

namespace Volta.BuildingBlocks.EventBus.IntegrationEventLog.Services
{
    public interface IIntegrationEventPublisher
    {
        Task Publish(IntegrationEvent evt);
    }
}
