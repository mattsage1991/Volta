using System;
using Volta.BuildingBlocks.Domain;
using Volta.BuildingBlocks.Domain.Entities;
using Volta.Stocks.Domain.Stocks.Events;
using Volta.Stocks.Domain.Stocks.Services;

namespace Volta.Stocks.Domain.Stocks
{
    public class Stock : Entity, IAggregateRoot
    {
        public StockId Id { get; }

        public string CompanyName { get; }

        public string Symbol { get; }

        public KeyStats KeyStats { get; private set; }

        private Stock() { }

        private Stock(StockId id, string companyName, string symbol, IStockLookup stockLookup)
        {
            if(string.IsNullOrEmpty(symbol))
                throw new ArgumentNullException(nameof(symbol), "Symbol must be specified");

            var keyStats = stockLookup.GetKeyStats(symbol).GetAwaiter().GetResult();

            Id = id;
            KeyStats = keyStats;
            CompanyName = companyName;
            Symbol = symbol;

            AddDomainEvent(new StockCreatedDomainEvent(Id, CompanyName, Symbol));
        }

        public static Stock CreateNew(StockId stockId, string companyName, string symbol, IStockLookup stockLookup)
        {
            return new Stock(stockId, companyName, symbol, stockLookup);
        }

        public void RefreshKeyStats(IStockLookup stockLookup)
        {
            var keyStats = stockLookup.GetKeyStats(Symbol).GetAwaiter().GetResult(); ;
            KeyStats = keyStats;

            AddDomainEvent(new StockKeyStatsUpdatedDomainEvent(Id));
        }

    }
}