using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks
{
    public class PeRatio : ValueObject
    {
        public decimal? Value { get; }

        private PeRatio() { }

        private PeRatio(decimal? value)
        {
            this.Value = value;
        }

        public static PeRatio Of(decimal? value)
        {
            return new PeRatio(value);
        }
    }
}