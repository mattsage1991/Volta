using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks.Events
{
    public class StockProfitMarginChangedDomainEvent : DomainEvent
    {
        public StockId StockId { get; }
        public ProfitMargin ProfitMargin { get; }

        public StockProfitMarginChangedDomainEvent(StockId stockId, ProfitMargin profitMargin)
        {
            StockId = stockId;
            ProfitMargin = profitMargin;
        }
    }
}