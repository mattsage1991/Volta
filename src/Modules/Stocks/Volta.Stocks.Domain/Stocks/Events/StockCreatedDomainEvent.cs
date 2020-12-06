using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks.Events
{
    public class StockCreatedDomainEvent : DomainEvent
    {
        public StockCreatedDomainEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}