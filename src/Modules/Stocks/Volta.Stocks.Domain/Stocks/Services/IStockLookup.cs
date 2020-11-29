using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks.Services
{
    public interface IStockLookup
    {
        KeyStats FindStock(string symbol);
    }

    public class KeyStats : ValueObject
    {
        public string Symbol { get; set; }
        public long MarketCap { get; set; }
        public decimal PeRatio { get; set; }
        public decimal PegRatio { get; set; }
        public decimal PriceToBookRatio { get; set; }
        public decimal ProfitMargin { get; set; }
        public long TotalRevenue { get; set; }
        public decimal DividendYield { get; set; }
        public static KeyStats None => new KeyStats();
    }
}