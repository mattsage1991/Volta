using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
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

        public async Task<KeyStats> GetKeyStats(string symbol)
        {
            var responseString = await _httpClient.GetStringAsync(
                $"https://sandbox.iexapis.com/stable/stock/{symbol}/advanced-stats?token=Tpk_e3d4d30d956e4241899ec245f92a0593");

            var advancedStats = JsonSerializer.Deserialize<AdvancedStatsResult>(responseString,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            return KeyStats.Of(advancedStats.Marketcap, advancedStats.TotalRevenue, advancedStats.DividendYield, advancedStats.PeRatio, 
                advancedStats.PegRatio, advancedStats.PriceToBook, advancedStats.ProfitMargin);
        }
    }
}