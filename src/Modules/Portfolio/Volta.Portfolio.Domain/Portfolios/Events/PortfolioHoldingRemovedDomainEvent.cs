using Volta.BuildingBlocks.Domain;
using Volta.Portfolios.Domain.Stocks;

namespace Volta.Portfolios.Domain.Portfolios.Events
{
    public class PortfolioHoldingRemovedDomainEvent : DomainEventBase
    {
        public PortfolioHoldingRemovedDomainEvent(StockId stockId, PortfolioId portfolioId)
        {
            StockId = stockId;
            PortfolioId = portfolioId;
        }

        public StockId StockId { get; }
        public PortfolioId PortfolioId { get; }
    }
}