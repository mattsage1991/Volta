using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volta.BuildingBlocks.EventBus.Abstractions;

namespace Volta.BuildingBlocks.EventBus.IntegrationEventLog.Services
{
    public class InternalIntegrationEventPublisher : IInternalIntegrationEventPublisher
    {
        private readonly IEventBus eventBus;
        private readonly IIntegrationEventAccessor integrationEventAccessor;
        private readonly ILogger<InternalIntegrationEventPublisher> logger;

        public InternalIntegrationEventPublisher(IEventBus eventBus,
            IIntegrationEventAccessor integrationEventAccessor,
            ILogger<InternalIntegrationEventPublisher> logger)
        {
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            this.integrationEventAccessor = integrationEventAccessor ?? throw new ArgumentNullException(nameof(integrationEventAccessor));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task PublishEventsThroughEventBusByTransactionId(Guid transactionId)
        {
            var pendingLogEvents = await integrationEventAccessor.GetUnpublishedIntegrationEventsByTransactionId(transactionId);
            await PublishEventsThroughEventsBus(pendingLogEvents);
        }

        public async Task PublishEventsThroughEventsBus()
        {
            var pendingLogEvents = await integrationEventAccessor.GetUnpublishedIntegrationEvents();
            await PublishEventsThroughEventsBus(pendingLogEvents);
        }

        private async Task PublishEventsThroughEventsBus(IEnumerable<IntegrationEventLogEntry> pendingLogEvents)
        {
            foreach (var logEvt in pendingLogEvents)
            {
                logger.LogInformation("----- Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", logEvt.EventId, logEvt.IntegrationEvent);

                try
                {
                    await integrationEventAccessor.MarkEventAsInProgress(logEvt.EventId);
                    eventBus.Publish(logEvt.IntegrationEvent);
                    await integrationEventAccessor.MarkEventAsPublished(logEvt.EventId);
                    logger.LogInformation("----- Published integration event: {IntegrationEventId} - ({@IntegrationEvent})", logEvt.EventId, logEvt.IntegrationEvent);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "ERROR publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", logEvt.EventId, logEvt.IntegrationEvent);

                    await integrationEventAccessor.MarkEventAsFailed(logEvt.EventId);
                }
            }
        }
    }
}
