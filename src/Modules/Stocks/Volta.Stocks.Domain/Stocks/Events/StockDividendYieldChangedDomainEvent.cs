using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks.Events
{
    public class StockDividendYieldChangedDomainEvent : DomainEvent
    {
        public StockId StockId { get; }
        public DividendYield DividendYield { get; }

        public StockDividendYieldChangedDomainEvent(StockId stockId, DividendYield dividendYield)
        {
            StockId = stockId;
            DividendYield = dividendYield;
        }
    }
}