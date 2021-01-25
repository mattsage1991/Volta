using Volta.BuildingBlocks.Domain;

namespace Volta.Portfolios.Domain.Portfolios
{
    public class PortfolioName : ValueObject
    {
        public string Value { get; }

        private PortfolioName() { }

        private PortfolioName(string value)
        {
            this.Value = value;
        }

        public static PortfolioName Of(string value)
        {
            return new PortfolioName(value);
        }
    }
}