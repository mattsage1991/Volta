using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volta.BuildingBlocks.EventBus;
using Volta.BuildingBlocks.EventBus.IntegrationEventLog;
using Volta.BuildingBlocks.EventBus.IntegrationEventLog.Services;

namespace Volta.BuildingBlocks.Infrastructure.EntityFramework.IntegrationEventServices
{
    public class IntegrationEventAccessor : IIntegrationEventAccessor
    {
        private readonly IntegrationEventLogContext eventLogContext;
        private readonly List<Type> eventTypes;

        public IntegrationEventAccessor(IntegrationEventLogContext eventLogContext, IIntegrationEventTypesProvider integrationEventsProvider)
        {
            if (integrationEventsProvider is null)
            {
                throw new ArgumentNullException(nameof(integrationEventsProvider));
            }

            this.eventLogContext = eventLogContext ?? throw new ArgumentNullException(nameof(eventLogContext));
            this.eventTypes = integrationEventsProvider.GetIntegrationEvents();
        }

        public async Task<IEnumerable<IntegrationEventLogEntry>> GetUnpublishedIntegrationEvents()
        {
            var result = await eventLogContext.IntegrationEventLogs.Where(x => x.State != EventStateEnum.Published).ToListAsync();


            if (result != null && result.Any())
            {
                return result.OrderBy(o => o.CreationTime)
                    .Select(e => e.DeserializeJsonContent(eventTypes.Find(t => t.Name == e.EventTypeShortName)));
            }

            return new List<IntegrationEventLogEntry>();
        }

        public async Task<IEnumerable<IntegrationEventLogEntry>> GetUnpublishedIntegrationEventsByTransactionId(Guid transactionId)
        {
            var tid = transactionId.ToString();

            var result = await eventLogContext.IntegrationEventLogs
                .Where(e => e.TransactionId == tid && e.State == EventStateEnum.NotPublished).ToListAsync();

            if (result != null && result.Any())
            {
                return result.OrderBy(o => o.CreationTime)
                    .Select(e => e.DeserializeJsonContent(eventTypes.Find(t => t.Name == e.EventTypeShortName)));
            }

            return new List<IntegrationEventLogEntry>();
        }

        public async Task MarkEventAsPublished(Guid eventId)
        {
            await UpdateEventStatus(eventId, EventStateEnum.Published);
        }

        public async Task MarkEventAsInProgress(Guid eventId)
        {
            await UpdateEventStatus(eventId, EventStateEnum.InProgress);
        }

        public async Task MarkEventAsFailed(Guid eventId)
        {
            await UpdateEventStatus(eventId, EventStateEnum.PublishedFailed);
        }

        private async Task UpdateEventStatus(Guid eventId, EventStateEnum status)
        {
            var eventLogEntry = eventLogContext.IntegrationEventLogs.Single(ie => ie.EventId == eventId);
            eventLogEntry.State = status;

            if (status == EventStateEnum.InProgress)
                eventLogEntry.TimesSent++;

            eventLogContext.IntegrationEventLogs.Update(eventLogEntry);

            await eventLogContext.SaveChangesAsync();
        }
    }
}
