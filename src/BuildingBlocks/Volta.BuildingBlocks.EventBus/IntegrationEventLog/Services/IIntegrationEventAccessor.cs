using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Volta.BuildingBlocks.EventBus.IntegrationEventLog.Services
{
    public interface IIntegrationEventAccessor
    {
        public Task<IEnumerable<IntegrationEventLogEntry>> GetUnpublishedIntegrationEvents();
        public Task<IEnumerable<IntegrationEventLogEntry>> GetUnpublishedIntegrationEventsByTransactionId(Guid transactionId);
        public Task MarkEventAsPublished(Guid eventId);
        public Task MarkEventAsInProgress(Guid eventId);
        public Task MarkEventAsFailed(Guid eventId);
    }
}
