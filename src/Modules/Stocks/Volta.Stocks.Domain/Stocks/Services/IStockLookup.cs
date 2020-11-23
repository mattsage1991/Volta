using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks.Services
{
    public interface IStockLookup
    {
        StockDetails FindStock(string symbol);
    }

    public class StockDetails : ValueObject
    {
        public long MarketCap { get; set; }
        public decimal PeRatio { get; set; }
        public decimal PegRatio { get; set; }
        public decimal PriceToBookRatio { get; set; }
        public decimal ProfitMargin { get; set; }
        public long TotalRevenue { get; set; }
        public decimal DividendYield { get; set; }
        public static StockDetails None => new StockDetails();
    }
}