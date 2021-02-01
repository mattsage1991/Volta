using System;
using FluentAssertions;
using FluentAssertions.Execution;
using Volta.Portfolios.Domain.Portfolios;
using Volta.Portfolios.Domain.Portfolios.Events;
using Volta.Portfolios.Domain.Portfolios.Rules;
using Volta.Portfolios.Domain.Stocks;
using Volta.Portfolios.Tests.UnitTests.SeedWork;
using Xunit;

namespace Volta.Portfolios.Domain.UnitTests.Portfolios
{
    public class PortfolioRemoveHoldingTests : TestBase
    {
        private readonly TestHarness harness;

        public PortfolioRemoveHoldingTests()
        {
            harness = new TestHarness();
        }

        [Fact]
        public void RemoveHolding_WhenStockIsAHolding_IsSuccessful()
        {
            // Arrange
            var portfolio = harness.CreatePortfolio();
            var stockId = new StockId(Guid.NewGuid());
            var averageCost = AverageCost.Of(MoneyValue.Of(10, "USD"));
            var numberOfShares = NumberOfShares.Of(100);
            var holdingId = portfolio.AddHolding(stockId, averageCost, numberOfShares);

            // Act
            portfolio.RemoveHolding(stockId);

            //Assert
            using var _ = new AssertionScope();

            var domainEvent = AssertPublishedDomainEvent<HoldingRemovedDomainEvent>(portfolio);
            domainEvent.HoldingId.Should().Be(holdingId);
        }

        [Fact]
        public void RemoveHolding_WhenStockIsNotAHolding_IsNotPossible()
        {
            // Arrange
            var portfolio = harness.CreatePortfolio();
            var stockId = new StockId(Guid.NewGuid());

            // Act
            Action act = () => portfolio.RemoveHolding(stockId);

            //Assert
            AssertBrokenRule<OnlyActiveHoldingCanBeRemovedFromPortfolioRule>(act);
        }
    }
}