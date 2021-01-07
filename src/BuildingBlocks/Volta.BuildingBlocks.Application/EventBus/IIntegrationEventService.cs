using System;
using System.Threading.Tasks;

namespace Volta.BuildingBlocks.Application.EventBus
{
    public interface IIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent @event);
    }
}