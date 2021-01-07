using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks.Events
{
    public class StockTotalRevenueChangedDomainEvent : DomainEvent
    {
        public StockId StockId { get; }
        public TotalRevenue TotalRevenue { get; }

        public StockTotalRevenueChangedDomainEvent(StockId stockId, TotalRevenue totalRevenue)
        {
            StockId = stockId;
            TotalRevenue = totalRevenue;
        }
    }
}