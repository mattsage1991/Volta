using System.Threading.Tasks;
using Volta.BuildingBlocks.EventBus.Events;

namespace Volta.BuildingBlocks.EventBus.IntegrationEventLog.Services
{
    public interface IIntegrationEventLogService
    {
        Task SaveEvent(IntegrationEvent @event, object transaction);
    }
}
