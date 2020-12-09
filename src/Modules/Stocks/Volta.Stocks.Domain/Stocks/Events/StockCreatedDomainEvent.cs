using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks.Events
{
    public class StockCreatedDomainEvent : DomainEvent
    {
        public StockCreatedDomainEvent(Guid id, string companyName, string symbol)
        {
            Id = id;
            CompanyName = companyName;
            Symbol = symbol;
        }

        public Guid Id { get; }
        public string CompanyName { get; }
        public string Symbol { get; }
    }
}