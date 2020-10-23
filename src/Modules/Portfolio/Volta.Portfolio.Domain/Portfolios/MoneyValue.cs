using Volta.BuildingBlocks.Domain;

namespace Volta.Portfolios.Domain.Portfolios
{
    public class MoneyValue : ValueObject
    {
        public decimal? Value { get; }

        public string Currency { get; }

        private MoneyValue(decimal? value, string currency)
        {
            this.Value = value;
            this.Currency = currency;
        }
    }
}