using System;
using NUnit.Framework;
using Volta.Portfolios.Domain.Portfolios;
using Volta.Portfolios.Domain.Portfolios.Events;
using Volta.Portfolios.Domain.Portfolios.Rules;
using Volta.Portfolios.Domain.Stocks;

namespace Volta.Portfolios.Tests.UnitTests.Portfolios
{
    public class PortfolioAddHoldingTests : PortfolioTestsBase
    {
        [Test]
        public void AddHolding_WhenHoldingAlreadyExistsInPortfolio_IsNotPossible()
        {
            var portfolioTestData = CreatePortfolioTestData(new HoldingTestDataOptions());

            var newHoldingId = new StockId(Guid.NewGuid());
            portfolioTestData.Portfolio.AddHolding(newHoldingId, MoneyValue.Undefined, 0);

            AssertBrokenRule<StockCannotBeAHoldingOfPortfolioMoreThanOnce>(() =>
            {
                portfolioTestData.Portfolio.AddHolding(newHoldingId, MoneyValue.Undefined,  0);
            });
        }


        [Test]
        public void AddHolding_WhenAllConditionsAllowsNewHolding_IsSuccessful()
        {
            var portfolioTestData = CreatePortfolioTestData(new HoldingTestDataOptions());

            var newHoldingId = new StockId(Guid.NewGuid());

            portfolioTestData.Portfolio.AddHolding(newHoldingId, MoneyValue.Of(1000,"gbp"), 10);

            var portfolioHoldingsAddedEvents =
                AssertPublishedDomainEvent<PortfolioHoldingAddedDomainEvent>(portfolioTestData.Portfolio);

            Assert.That(portfolioHoldingsAddedEvents.StockId, Is.EqualTo(newHoldingId));
        }
    }
}