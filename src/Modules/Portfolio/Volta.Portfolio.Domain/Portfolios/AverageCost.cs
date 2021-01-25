using Volta.BuildingBlocks.Domain;

namespace Volta.Portfolios.Domain.Portfolios
{
    public class AverageCost : ValueObject
    {
        public MoneyValue Value { get; }

        private AverageCost() { }

        private AverageCost(MoneyValue value)
        {
            this.Value = value;
        }

        public static AverageCost Of(MoneyValue value)
        {
            return new AverageCost(value);
        }
    }
}