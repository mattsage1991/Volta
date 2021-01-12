using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks
{
    public class PegRatio : ValueObject
    {
        public decimal? Value { get; }

        private PegRatio() { }

        private PegRatio(decimal? value)
        {
            this.Value = value;
        }

        public static PegRatio Of(decimal? value)
        {
            return new PegRatio(value);
        }
    }
}