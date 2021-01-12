using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks
{
    public class ProfitMargin : ValueObject
    {
        public decimal Value { get; }

        private ProfitMargin() { }

        private ProfitMargin(decimal value)
        {
            this.Value = value;
        }

        public static ProfitMargin Of(decimal value)
        {
            return new ProfitMargin(value);
        }
    }
}