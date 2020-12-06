using System;
using Volta.BuildingBlocks.Domain;
using Volta.BuildingBlocks.Domain.Entities;
using Volta.Stocks.Domain.Stocks.Events;
using Volta.Stocks.Domain.Stocks.Services;

namespace Volta.Stocks.Domain.Stocks
{
    public class Stock : Entity
    {
        public StockId Id { get; }

        public KeyStats KeyStats { get; private set; }

        private Stock() { }

        private Stock(StockId id, string companyName, string symbol, IStockLookup stockLookup)
        {
            if(string.IsNullOrEmpty(symbol))
                throw new ArgumentNullException(nameof(symbol), "Symbol must be specified");

            var keyStats = stockLookup.FindStock(symbol);

            Id = id;
            KeyStats = keyStats;
            _companyName = companyName;

            AddDomainEvent(new StockCreatedDomainEvent(Id));
        }

        public static Stock CreateNew(StockId stockId, string companyName, string symbol, IStockLookup stockLookup)
        {
            return new Stock(stockId, companyName, symbol, stockLookup);
        }

        public void RefreshKeyStats(IStockLookup stockLookup)
        {
            var keyStats = stockLookup.FindStock(KeyStats.Symbol);
            KeyStats = keyStats;

            AddDomainEvent(new StockKeyStatsUpdatedDomainEvent(Id));
        }
        
        private string _companyName;
    }
}