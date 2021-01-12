using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks.Events
{
    public class StockCreatedDomainEvent : DomainEvent
    {
        public StockId StockId { get; }
        public CompanyName CompanyName { get; }
        public TickerSymbol TickerSymbol { get; }

        public StockCreatedDomainEvent(StockId stockId, CompanyName companyName, TickerSymbol tickerSymbol)
        {
            StockId = stockId;
            CompanyName = companyName;
            TickerSymbol = tickerSymbol;
        }
    }
}