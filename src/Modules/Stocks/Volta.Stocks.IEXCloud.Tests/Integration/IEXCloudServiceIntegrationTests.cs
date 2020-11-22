using System.Net.Http;
using System.Threading.Tasks;
using Volta.Stocks.IEXCloud.Services;
using Xunit;

namespace Volta.Stocks.IEXCloud.Tests.Integration
{

    public class IEXCloudServiceIntegrationTests
    {
        [Fact]
        public async Task When_retrieving_advanced_stats_from_service()
        {
            using var httpClient = new HttpClient();
            var service = new IEXCloudService(httpClient);

            var advancedStats = await service.GetAdvancedStats("msft");

            Assert.NotEqual(default, advancedStats.CompanyName);
            Assert.NotEqual(default, advancedStats.PeRatio);
            Assert.NotEqual(default, advancedStats.DebtToEquity);
            Assert.NotEqual(default, advancedStats.DividendYield);
            Assert.NotEqual(default, advancedStats.Marketcap);
            Assert.NotEqual(default, advancedStats.Month3ChangePercent);
            Assert.NotEqual(default, advancedStats.Month6ChangePercent);
            Assert.NotEqual(default, advancedStats.PegRatio);
            Assert.NotEqual(default, advancedStats.PriceToBook);
            Assert.NotEqual(default, advancedStats.ProfitMargin);
            Assert.NotEqual(default, advancedStats.TotalRevenue);
            Assert.NotEqual(default, advancedStats.YtdChangePercent);
        }
    }
}