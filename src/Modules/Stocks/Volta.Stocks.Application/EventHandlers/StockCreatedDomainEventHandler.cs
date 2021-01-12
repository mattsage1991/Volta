using System;
using System.Threading;
using System.Threading.Tasks;
using Volta.BuildingBlocks.Domain.Events;
using Volta.BuildingBlocks.EventBus.IntegrationEventLog.Services;
using Volta.Stocks.Domain.Stocks.Events;
using Volta.Stocks.IntegrationEvents;

namespace Volta.Stocks.Application.EventHandlers
{
    public class StockCreatedDomainEventHandler : IDomainEventHandler<StockCreatedDomainEvent>
    {
        private readonly IIntegrationEventPublisher eventPublisher;

        public StockCreatedDomainEventHandler(IIntegrationEventPublisher eventPublisher)
        {
            this.eventPublisher = eventPublisher;
        }

        public async Task Handle(StockCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await eventPublisher.Publish(new StockCreatedIntegrationEvent(notification.StockId.Value,
                notification.CompanyName.Value,
                notification.TickerSymbol.Value));
        }
    }
}