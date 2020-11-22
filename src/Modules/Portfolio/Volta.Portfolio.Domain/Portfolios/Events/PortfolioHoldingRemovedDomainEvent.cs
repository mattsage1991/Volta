using Volta.BuildingBlocks.Domain;
using Volta.Portfolios.Domain.Stocks;

namespace Volta.Portfolios.Domain.Portfolios.Events
{
    public class PortfolioHoldingRemovedDomainEvent : DomainEventBase
    {
        public PortfolioHoldingRemovedDomainEvent(HoldingId holdingId, PortfolioId portfolioId)
        {
            HoldingId = holdingId;
            PortfolioId = portfolioId;
        }

        public HoldingId HoldingId { get; }
        public PortfolioId PortfolioId { get; }
    }
}