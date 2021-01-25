using System;
using Volta.Portfolios.Domain.Portfolios;

namespace Volta.Portfolios.Domain.UnitTests.Portfolios
{
    public class TestHarness
    {
        public Portfolio CreatePortfolio()
        {
            var portfolio = Portfolio.Create(MemberId.Of(Guid.NewGuid()), PortfolioName.Of("name"));
            return portfolio;
        }
    }
}