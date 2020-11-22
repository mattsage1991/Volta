using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks
{
    public class Stock : Entity
    {
        public StockId Id { get; }

        public Stock(StockId id, string companyName, string symbol)
        {
            Id = id;
        }

        public void SetMarketCap(long marketCap) => _marketCap = marketCap;

        public void SetPeRatio(decimal peRatio) => _peRatio = peRatio;

        public void SetPegRatio(decimal pegRatio) => _pegRatio = pegRatio;

        public void SetPriceToBookRatio(decimal priceToBookRatio) => _priceToBookRatio = priceToBookRatio;

        public void SetProfitMargin(decimal profitMargin) => _profitMargin = profitMargin;

        public void SetTotalRevenue(long totalRevenue) => _totalRevenue = totalRevenue;

        public void SetDividendYield(decimal dividendYield) => _dividendYield = dividendYield;

        private string _companyName;
        private string _symbol;
        private long _marketCap;
        private decimal _peRatio;
        private decimal _pegRatio;
        private decimal _priceToBookRatio;
        private decimal _profitMargin;
        private long _totalRevenue;
        private decimal _dividendYield;
    }
}