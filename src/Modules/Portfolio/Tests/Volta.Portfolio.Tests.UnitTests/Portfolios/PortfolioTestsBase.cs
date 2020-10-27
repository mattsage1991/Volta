using System.Collections.Generic;
using System.Linq;
using Volta.Portfolios.Domain.Holding;
using Volta.Portfolios.Domain.Portfolios;
using Volta.Portfolios.Tests.UnitTests.SeedWork;

namespace Volta.Portfolios.Tests.UnitTests.Portfolios
{
    public class PortfolioTestsBase : TestBase
    {
        protected class HoldingTestDataOptions
        {
            public IEnumerable<StockId> Holdings { get; set; } = Enumerable.Empty<StockId>();
        }

        protected class PortfolioTestData
        {
            public PortfolioTestData(Portfolio portfolio)
            {
                Portfolio = portfolio;
            }

            public Portfolio Portfolio { get; }
        }

        protected PortfolioTestData CreatePortfolioTestData(HoldingTestDataOptions options)
        {
            var portfolio = Portfolio.CreateNew("name");

            foreach (var holding in options.Holdings)
            {
                portfolio.AddHolding(holding, MoneyValue.Of(1000,"gbp"), 1);
            }

            return new PortfolioTestData(portfolio);
        }
    }
}