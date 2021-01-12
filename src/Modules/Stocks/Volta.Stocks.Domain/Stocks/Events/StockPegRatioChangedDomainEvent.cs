using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks.Events
{
    public class StockPegRatioChangedDomainEvent : DomainEvent
    {
        public StockId StockId { get; }
        public PegRatio PegRatio { get; }

        public StockPegRatioChangedDomainEvent(StockId stockId, PegRatio pegRatio)
        {
            StockId = stockId;
            PegRatio = pegRatio;
        }
    }
}