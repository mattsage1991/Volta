using System;
using Volta.BuildingBlocks.Domain;
using Volta.Stocks.Domain.Stocks.Services;

namespace Volta.Stocks.Domain.Stocks
{
    public class Stock : Entity
    {
        public StockId Id { get; }

        public KeyStats KeyStats { get; private set; }

        public Stock(StockId id, string companyName, string symbol, IStockLookup stockLookup)
        {
            if(string.IsNullOrEmpty(symbol))
                throw new ArgumentNullException(nameof(symbol), "Symbol must be specified");

            var keyStats = stockLookup.FindStock(symbol);

            Id = id;
            KeyStats = keyStats;
            _companyName = companyName;

            Raise(new Events.StockCreated
            {
                Id = id
            });
        }

        public void RefreshKeyStats(IStockLookup stockLookup)
        {
            var keyStats = stockLookup.FindStock(KeyStats.Symbol);
            KeyStats = keyStats;

            Raise(new Events.StockKeyStatsUpdated
            {
                Id = Id
            });
        }
        
        private string _companyName;
    }
}