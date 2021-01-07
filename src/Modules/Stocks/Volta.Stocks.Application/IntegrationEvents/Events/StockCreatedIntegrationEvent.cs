using System;
using Volta.BuildingBlocks.Application.EventBus;

namespace Volta.Stocks.Application.IntegrationEvents.Events
{
    public class StockCreatedIntegrationEvent : IntegrationEvent
    {
        public StockCreatedIntegrationEvent(string companyName, string symbol)
        {
            CompanyName = companyName;
            Symbol = symbol;
        }

        public string CompanyName { get; }
        public string Symbol { get; }
    }
}