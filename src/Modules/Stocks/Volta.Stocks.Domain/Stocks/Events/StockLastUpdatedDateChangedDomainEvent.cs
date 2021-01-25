using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks.Events
{
    public class StockLastUpdatedDateChangedDomainEvent : DomainEvent
    {
        public StockId StockId { get; }
        public DateTime LastUpdatedDate { get; }

        public StockLastUpdatedDateChangedDomainEvent(StockId stockId, DateTime lastUpdatedDate)
        {
            StockId = stockId;
            LastUpdatedDate = lastUpdatedDate;
        }
    }
}