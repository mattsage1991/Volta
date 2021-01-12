using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks
{
    public class PriceToBookRatio : ValueObject
    {
        public decimal? Value { get; }

        private PriceToBookRatio() { }

        private PriceToBookRatio(decimal? value)
        {
            this.Value = value;
        }

        public static PriceToBookRatio Of(decimal? value)
        {
            return new PriceToBookRatio(value);
        }
    }
}