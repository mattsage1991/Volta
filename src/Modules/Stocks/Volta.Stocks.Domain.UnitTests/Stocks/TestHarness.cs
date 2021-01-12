using System.Threading.Tasks;
using Moq;
using Volta.Stocks.Domain.Stocks;
using Volta.Stocks.Domain.Stocks.Services;

namespace Volta.Stocks.Domain.UnitTests.Stocks
{
    public class TestHarness
    {
        public IStockLookup StockLookup
        {
            get
            {
                var stockLookupMock = new Mock<IStockLookup>();
                stockLookupMock.Setup(x => x.GetLiveStockData(It.IsAny<TickerSymbol>())).Returns(Task.FromResult(CreateLiveStockData()));
                return stockLookupMock.Object;
            }
        }

        private LiveStockData CreateLiveStockData()
        {
            var stockData = LiveStockData.Of(
                MarketCap.Of(11111111), 
                PeRatio.Of(1), 
                PegRatio.Of(1), 
                PriceToBookRatio.Of(1), 
                ProfitMargin.Of(0.1m),
                TotalRevenue.Of(10000), 
                DividendYield.Of(0.15m));

            return stockData;
        }
    }
}