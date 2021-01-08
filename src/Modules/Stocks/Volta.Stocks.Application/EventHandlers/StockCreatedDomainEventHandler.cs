using System;
using System.Threading;
using System.Threading.Tasks;
using Volta.BuildingBlocks.Application.EventBus;
using Volta.BuildingBlocks.Domain.Events;
using Volta.Stocks.Application.IntegrationEvents.Events;
using Volta.Stocks.Domain.Stocks.Events;

namespace Volta.Stocks.Application.EventHandlers
{
    public class StockCreatedDomainEventHandler : IDomainEventHandler<StockCreatedDomainEvent>
    {
        private readonly IIntegrationEventService _integrationEventService;

        public StockCreatedDomainEventHandler(IIntegrationEventService integrationEventService)
        {
            _integrationEventService = integrationEventService ?? throw new ArgumentNullException(nameof(integrationEventService));
        }

        public async Task Handle(StockCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}