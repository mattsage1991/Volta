using System.Threading;
using System.Threading.Tasks;
using Volta.BuildingBlocks.Domain.Events;
using Volta.Stocks.Domain.Stocks.Events;

namespace Volta.Stocks.Application.EventHandlers
{
    public class StockCreatedDomainEventHandler : IDomainEventHandler<StockCreatedDomainEvent>
    {
        public StockCreatedDomainEventHandler()
        {
            
        }

        public Task Handle(StockCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}