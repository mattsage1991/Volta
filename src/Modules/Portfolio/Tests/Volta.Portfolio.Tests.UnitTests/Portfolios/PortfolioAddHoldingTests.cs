using System;
using NUnit.Framework;
using Volta.Portfolios.Domain.Holding;
using Volta.Portfolios.Domain.Portfolios;
using Volta.Portfolios.Domain.Portfolios.Events;

namespace Volta.Portfolios.Tests.UnitTests.Portfolios
{
    public class PortfolioAddHoldingTests : PortfolioTestsBase
    {
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