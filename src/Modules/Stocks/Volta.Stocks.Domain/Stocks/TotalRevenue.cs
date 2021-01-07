using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks
{
    public class TotalRevenue : ValueObject
    {
        public long Value { get; }

        private TotalRevenue() { }

        private TotalRevenue(long value)
        {
            this.Value = value;
        }

        public static TotalRevenue Of(long value)
        {
            return new TotalRevenue(value);
        }
    }
}