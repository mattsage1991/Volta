using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks.Events
{
    public class StockCreatedDomainEvent : DomainEvent
    {
        public StockId Id { get; }
        public CompanyName CompanyName { get; }
        public TickerSymbol TickerSymbol { get; }

        public StockCreatedDomainEvent(StockId id, CompanyName companyName, TickerSymbol tickerSymbol)
        {
            Id = id;
            CompanyName = companyName;
            TickerSymbol = tickerSymbol;
        }
    }
}