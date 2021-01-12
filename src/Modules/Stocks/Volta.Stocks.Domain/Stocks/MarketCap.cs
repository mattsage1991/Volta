using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks
{
    public class MarketCap : ValueObject
    {
        public long Value { get; }

        private MarketCap() { }

        private MarketCap(long value)
        {
            this.Value = value;
        }

        public static MarketCap Of(long value)
        {
            return new MarketCap(value);
        }
    }
}