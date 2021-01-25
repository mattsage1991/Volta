using System;
using Volta.BuildingBlocks.Domain;
using Volta.Portfolios.Domain.Stocks;

namespace Volta.Portfolios.Domain.Portfolios.Events
{
    public class PortfolioHoldingAddedDomainEvent : DomainEvent
    {
        public PortfolioId PortfolioId { get; set; }
        public StockId StockId { get; set; }
        public AverageCost AverageCost { get; set; }
        public NumberOfShares NumberOfShares { get; set; }

        public PortfolioHoldingAddedDomainEvent(PortfolioId portfolioId, StockId stockId, AverageCost averageCost, NumberOfShares numberOfShares)
        {
            PortfolioId = portfolioId;
            StockId = stockId;
            AverageCost = averageCost;
            NumberOfShares = numberOfShares;
        }
    }
}