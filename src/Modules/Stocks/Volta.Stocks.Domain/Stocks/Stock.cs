using System;
using Volta.BuildingBlocks.Domain;
using Volta.Stocks.Domain.Stocks.Services;

namespace Volta.Stocks.Domain.Stocks
{
    public class Stock : Entity
    {
        public StockId Id { get; }

        public StockDetails StockDetails { get; }

        public Stock(StockId id, string companyName, string symbol, IStockLookup stockLookup)
        {
            if(string.IsNullOrEmpty(symbol))
                throw new ArgumentNullException(nameof(symbol), "Symbol must be specified");

            var stockDetails = stockLookup.FindStock(symbol);

            Id = id;
            StockDetails = stockDetails;
            _companyName = companyName;
            _symbol = symbol;
        }
        
        private string _companyName;
        private string _symbol;
    }
}