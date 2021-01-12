using System;
using Volta.BuildingBlocks.EventBus.Events;

namespace Volta.Stocks.IntegrationEvents
{
    public class StockCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid StockId { get; }
        public string CompanyName { get; }
        public string TickerSymbol { get; }

        public StockCreatedIntegrationEvent(Guid stockId, string companyName, string tickerSymbol)
        {
            StockId = stockId;
            CompanyName = companyName;
            TickerSymbol = tickerSymbol;
        }
    }
}