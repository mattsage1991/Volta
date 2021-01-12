using System;
using Volta.Portfolios.Domain.Portfolios;
using Volta.Portfolios.Domain.Portfolios.Events;
using Volta.Portfolios.Domain.Portfolios.Rules;
using Volta.Portfolios.Domain.Stocks;
using Xunit;

namespace Volta.Portfolios.Tests.UnitTests.Portfolios
{
    public class PortfolioTests : PortfolioTestsBase
    {
        //[Fact]
        //public void RemoveHolding_WhenStockExistsInPortfolio_IsSuccessful()
        //{
        //    var portfolioTestData = CreatePortfolioTestData(new HoldingTestDataOptions());

        //    var holdingToRemoveId = new HoldingId(Guid.NewGuid());
        //    portfolioTestData.Portfolio.AddHolding(holdingToRemoveId, MoneyValue.Of(10,"gbp"), 1);

        //    portfolioTestData.Portfolio.RemoveHolding(holdingToRemoveId);

        //    var portfolioHoldingRemoved = AssertPublishedDomainEvent<PortfolioHoldingRemovedDomainEvent>(portfolioTestData.Portfolio);
        //    Assert.That(portfolioHoldingRemoved.HoldingId, Is.EqualTo(holdingToRemoveId));
        //    Assert.That(portfolioHoldingRemoved.PortfolioId, Is.EqualTo(portfolioTestData.Portfolio.Id));
        //}

        [Fact]
        public void RemoveHolding_WhenStockIsNotActiveInPortfolio_BreaksOnlyExistingHoldingCanBeRemovedFromPortfolioRule()
        {
            var portfolioTestData = CreatePortfolioTestData(new HoldingTestDataOptions());

            var holdingToRemoveId = new HoldingId(Guid.NewGuid());

            AssertBrokenRule<OnlyActiveHoldingCanBeRemovedFromPortfolioRule>(() =>
            {
                portfolioTestData.Portfolio.RemoveHolding(holdingToRemoveId);
            });
        }
    }
}