using System;
using System.Threading.Tasks;

namespace Volta.BuildingBlocks.EventBus.IntegrationEventLog.Services
{
    public interface IInternalIntegrationEventPublisher
    {
        Task PublishEventsThroughEventBusByTransactionId(Guid transactionId);
        Task PublishEventsThroughEventsBus();
    }
}
