using System;
using FluentAssertions;
using FluentAssertions.Execution;
using Volta.Portfolios.Domain.Portfolios;
using Volta.Portfolios.Domain.Portfolios.Events;
using Volta.Portfolios.Domain.Stocks;
using Volta.Portfolios.Tests.UnitTests.SeedWork;
using Xunit;

namespace Volta.Portfolios.Domain.UnitTests.Portfolios
{
    public class PortfolioAddHoldingTests : TestBase
    {
        private TestHarness harness;

        public PortfolioAddHoldingTests()
        {
            harness = new TestHarness();
        }

        [Fact]
        public void AddHolding_WhenAllConditionsAllowsNewHolding_IsSuccessful()
        {
            // Arrange
            var portfolio = harness.CreatePortfolio();
            var stockId = new StockId(Guid.NewGuid());
            var averageCost = AverageCost.Of(MoneyValue.Of(10, "USD"));
            var numberOfShares = NumberOfShares.Of(100);

            // Act
            portfolio.AddHolding(stockId, averageCost, numberOfShares);

            //Assert
            using var _ = new AssertionScope();

            var domainEvent = AssertPublishedDomainEvent<PortfolioHoldingAddedDomainEvent>(portfolio);
            domainEvent.PortfolioId.Should().Be(portfolio.Id);
            domainEvent.StockId.Should().Be(stockId);
            domainEvent.AverageCost.Should().Be(averageCost);
            domainEvent.NumberOfShares.Should().Be(numberOfShares);
        }
    }
}