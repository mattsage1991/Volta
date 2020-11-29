using System.Collections.Generic;
using System.Linq;
using Volta.Stocks.Domain.Stocks.Services;

namespace Volta.Stocks.Tests.UnitTests
{
    public class FakeStockLookup : IStockLookup
    {
        private static readonly IEnumerable<KeyStats> Stocks = new[]
        {
            new KeyStats
            {
                Symbol = "MSFT",
                DividendYield = 0.82m,
                MarketCap = 1500000000,
                PegRatio = 1.7m,
                PeRatio = 29.4m,
                PriceToBookRatio = 3.3m,
                ProfitMargin = 20.2m,
                TotalRevenue = 1320000000
            },
            new KeyStats
            {
                Symbol = "AAPL",
                DividendYield = 1.41m,
                MarketCap = 1990000000,
                PegRatio = 2.7m,
                PeRatio = 39.4m,
                PriceToBookRatio = 3.3m,
                ProfitMargin = 20.2m,
                TotalRevenue = 1460000000
            }
        };

        public KeyStats FindStock(string symbol)
        {
            var stock = Stocks.FirstOrDefault(x => x.Symbol == symbol);
            return stock ?? KeyStats.None;
        }
    }
}