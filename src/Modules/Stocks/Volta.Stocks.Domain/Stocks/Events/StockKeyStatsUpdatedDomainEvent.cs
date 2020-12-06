using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks.Events
{
    public class StockKeyStatsUpdatedDomainEvent : DomainEvent
    {
        public StockKeyStatsUpdatedDomainEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}