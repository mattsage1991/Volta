using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks.Events
{
    public class StockKeyStatsUpdatedDomainEvent : DomainEvent
    {
        public StockKeyStatsUpdatedDomainEvent(StockId id)
        {
            Id = id;
        }

        public StockId Id { get; }
    }
}