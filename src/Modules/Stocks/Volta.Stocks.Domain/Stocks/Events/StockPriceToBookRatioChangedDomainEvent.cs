using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks.Events
{
    public class StockPriceToBookRatioChangedDomainEvent : DomainEvent
    {
        public StockId StockId { get; }
        public PriceToBookRatio PriceToBookRatio { get; }

        public StockPriceToBookRatioChangedDomainEvent(StockId stockId, PriceToBookRatio priceToBookRatio)
        {
            StockId = stockId;
            PriceToBookRatio = priceToBookRatio;
        }
    }
}