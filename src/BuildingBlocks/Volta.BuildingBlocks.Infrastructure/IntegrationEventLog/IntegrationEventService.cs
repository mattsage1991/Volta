using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Volta.BuildingBlocks.Application.EventBus;
using Volta.BuildingBlocks.Infrastructure.EventBus;

namespace Volta.BuildingBlocks.Infrastructure.IntegrationEventLog
{
    public class IntegrationEventService<T> : IIntegrationEventService 
        where T : DbContext
    {
        private readonly UnitOfWork<T> _unitOfWork;
        private readonly ILogger<T> _logger;
        private readonly IEventBus _eventBus;
        private readonly IIntegrationEventLogService _eventLogService;


        public IntegrationEventService(ILogger<T> logger, IEventBus eventBus, IIntegrationEventLogService eventLogService, UnitOfWork<T> unitOfWork)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = eventLogService ?? throw new ArgumentNullException(nameof(eventLogService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            var pendingLogEvents = await _eventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

            foreach (var logEvt in pendingLogEvents)
            {
                _logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from - ({@IntegrationEvent})", logEvt.EventId, logEvt.IntegrationEvent);

                try
                {
                    await _eventLogService.MarkEventAsInProgressAsync(logEvt.EventId);
                    _eventBus.Publish(logEvt.IntegrationEvent);
                    await _eventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "ERROR publishing integration event: {IntegrationEventId}", logEvt.EventId);

                    await _eventLogService.MarkEventAsFailedAsync(logEvt.EventId);
                }
            }
        }

        public async Task AddAndSaveEventAsync(IntegrationEvent @event)
        {
            _logger.LogInformation("----- Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", @event.Id, @event);

            await _eventLogService.SaveEventAsync(@event, _unitOfWork.GetCurrentTransaction());
        }
    }
}