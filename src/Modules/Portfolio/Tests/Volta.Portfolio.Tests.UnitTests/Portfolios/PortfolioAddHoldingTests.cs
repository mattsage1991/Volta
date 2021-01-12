using System;
using Volta.Portfolios.Domain.Portfolios;
using Volta.Portfolios.Domain.Portfolios.Events;
using Volta.Portfolios.Domain.Portfolios.Rules;
using Volta.Portfolios.Domain.Stocks;
using Xunit;

namespace Volta.Portfolios.Tests.UnitTests.Portfolios
{
    public class PortfolioAddHoldingTests : PortfolioTestsBase
    {
        [Fact]
        public void WhenHoldingAlreadyExistsInPortfolio_IsNotPossible()
        {
            var portfolioTestData = CreatePortfolioTestData(new HoldingTestDataOptions());

            var newHoldingId = new HoldingId(Guid.NewGuid());
            portfolioTestData.Portfolio.AddHolding(newHoldingId, MoneyValue.Undefined, 0);

            AssertBrokenRule<StockCannotBeAHoldingOfPortfolioMoreThanOnce>(() =>
            {
                portfolioTestData.Portfolio.AddHolding(newHoldingId, MoneyValue.Undefined,  0);
            });
        }


        //[Fact]
        //public void WhenAllConditionsAllowsNewHolding_IsSuccessful()
        //{
        //    var portfolioTestData = CreatePortfolioTestData(new HoldingTestDataOptions());

        //    var newHoldingId = new HoldingId(Guid.NewGuid());

        //    portfolioTestData.Portfolio.AddHolding(newHoldingId, MoneyValue.Of(1000,"gbp"), 10);

        //    var portfolioHoldingsAddedEvents =
        //        AssertPublishedDomainEvent<PortfolioHoldingAddedDomainEvent>(portfolioTestData.Portfolio);

        //    Assert.That(portfolioHoldingsAddedEvents.HoldingId, Is.EqualTo(newHoldingId));
        //}
    }
}