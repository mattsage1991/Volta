using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks.Events
{
    public class StockPeRatioChangedDomainEvent : DomainEvent
    {
        public StockId StockId { get; }
        public PeRatio PeRatio { get; }

        public StockPeRatioChangedDomainEvent(StockId stockId, PeRatio peRatio)
        {
            StockId = stockId;
            PeRatio = peRatio;
        }
    }
}