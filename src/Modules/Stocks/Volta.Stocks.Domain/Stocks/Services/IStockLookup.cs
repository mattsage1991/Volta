using System.Threading.Tasks;
using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks.Services
{
    public interface IStockLookup
    {
        Task<KeyStats> GetKeyStats(TickerSymbol symbol);
    }

    public class KeyStats
    {
        public MarketCap MarketCap { get; }
        public PeRatio PeRatio { get; }
        public PegRatio PegRatio { get; }
        public PriceToBookRatio PriceToBookRatio { get; }
        public ProfitMargin ProfitMargin { get; }
        public TotalRevenue TotalRevenue { get; }
        public DividendYield DividendYield { get; }

        private KeyStats(MarketCap marketCap, PeRatio peRatio, PegRatio pegRatio, PriceToBookRatio priceToBookRatio,
            ProfitMargin profitMargin, TotalRevenue totalRevenue, DividendYield dividendYield)
        {
            this.MarketCap = marketCap;
            this.TotalRevenue = totalRevenue;
            this.DividendYield = dividendYield;
            this.PeRatio = peRatio;
            this.PegRatio = pegRatio;
            this.PriceToBookRatio = priceToBookRatio;
            this.ProfitMargin = profitMargin;
        }

        public static KeyStats Of(MarketCap marketCap, PeRatio peRatio, PegRatio pegRatio, PriceToBookRatio priceToBookRatio,
            ProfitMargin profitMargin, TotalRevenue totalRevenue, DividendYield dividendYield)
        {
            return new KeyStats(marketCap, peRatio, pegRatio, priceToBookRatio, profitMargin, totalRevenue, dividendYield);
        }
    }
}