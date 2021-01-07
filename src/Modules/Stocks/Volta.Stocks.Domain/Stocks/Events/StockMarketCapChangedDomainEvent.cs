using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks.Events
{
    public class StockMarketCapChangedDomainEvent : DomainEvent
    {
        public StockId StockId { get; }
        public MarketCap MarketCap { get; }

        public StockMarketCapChangedDomainEvent(StockId stockId, MarketCap marketCap)
        {
            StockId = stockId;
            MarketCap = marketCap;
        }
    }
}