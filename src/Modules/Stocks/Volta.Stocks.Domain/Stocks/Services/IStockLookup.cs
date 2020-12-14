using System.Threading.Tasks;
using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks.Services
{
    public interface IStockLookup
    {
        Task<KeyStats> GetKeyStats(string symbol);
    }

    public class KeyStats : ValueObject
    {
        public long MarketCap { get; }
        public decimal? PeRatio { get; }
        public decimal PegRatio { get; }
        public decimal PriceToBookRatio { get; }
        public decimal ProfitMargin { get; }
        public long TotalRevenue { get; }
        public decimal? DividendYield { get; }

        private KeyStats(long marketCap, long totalRevenue, decimal? dividendYield, decimal? peRatio, 
            decimal pegRatio, decimal priceToBookRatio, decimal profitMargin)
        {
            MarketCap = marketCap;
            TotalRevenue = totalRevenue;
            DividendYield = dividendYield;
            PeRatio = peRatio;
            PegRatio = pegRatio;
            PriceToBookRatio = priceToBookRatio;
            ProfitMargin = profitMargin;
        }

        public static KeyStats Of(long marketCap, long totalRevenue, decimal? dividendYield, decimal? peRatio,
            decimal pegRatio, decimal priceToBookRatio, decimal profitMargin)
        {
            return new KeyStats(marketCap, totalRevenue, dividendYield, peRatio, pegRatio, priceToBookRatio, profitMargin);
        }
    }
}