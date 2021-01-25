using Volta.BuildingBlocks.Domain;

namespace Volta.Portfolios.Domain.Portfolios
{
    public class NumberOfShares : ValueObject
    {
        public double Value { get; }

        private NumberOfShares() { }

        private NumberOfShares(double value)
        {
            this.Value = value;
        }

        public static NumberOfShares Of(double value)
        {
            return new NumberOfShares(value);
        }
    }
}