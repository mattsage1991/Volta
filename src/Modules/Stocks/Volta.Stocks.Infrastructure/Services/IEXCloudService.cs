using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Volta.Stocks.Domain.Stocks;
using Volta.Stocks.Domain.Stocks.Services;
using Volta.Stocks.Infrastructure.Models;

namespace Volta.Stocks.Infrastructure.Services
{
    public class IEXCloudService : IStockLookup
    {
        private readonly HttpClient _httpClient;

        public IEXCloudService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<KeyStats> GetKeyStats(TickerSymbol symbol)
        {
            var responseString = await _httpClient.GetStringAsync(
                $"https://sandbox.iexapis.com/stable/stock/{symbol.Value}/advanced-stats?token=Tpk_e3d4d30d956e4241899ec245f92a0593");

            var advancedStats = JsonSerializer.Deserialize<AdvancedStatsResult>(responseString,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            return KeyStats.Of(MarketCap.Of(advancedStats.Marketcap), PeRatio.Of(advancedStats.PeRatio), PegRatio.Of(advancedStats.PegRatio), 
                PriceToBookRatio.Of(advancedStats.PriceToBook), ProfitMargin.Of(advancedStats.ProfitMargin), TotalRevenue.Of(advancedStats.TotalRevenue), 
                DividendYield.Of(advancedStats.DividendYield));
        }
    }
}