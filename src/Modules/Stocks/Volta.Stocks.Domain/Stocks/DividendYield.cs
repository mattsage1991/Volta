using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks
{
    public class DividendYield : ValueObject
    {
        public decimal? Value { get; }

        private DividendYield() { }

        private DividendYield(decimal? value)
        {
            this.Value = value;
        }

        public static DividendYield Of(decimal? value)
        {
            return new DividendYield(value);
        }
    }
}