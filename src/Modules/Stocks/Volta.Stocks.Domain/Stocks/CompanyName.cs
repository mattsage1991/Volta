using Volta.BuildingBlocks.Domain;

namespace Volta.Stocks.Domain.Stocks
{
    public class CompanyName : ValueObject
    {
        public string Value { get; }

        private CompanyName() { }

        private CompanyName(string value)
        {
            this.Value = value;
        }

        public static CompanyName Of(string value)
        {
            return new CompanyName(value);
        }
    }
}