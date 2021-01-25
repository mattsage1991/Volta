using System;
using FluentAssertions;
using FluentAssertions.Execution;
using Volta.Portfolios.Domain.Portfolios;
using Volta.Portfolios.Domain.Portfolios.Events;
using Volta.Portfolios.Tests.UnitTests.SeedWork;
using Xunit;

namespace Volta.Portfolios.Domain.UnitTests.Portfolios
{
    public class PortfolioTests : TestBase
    {
        [Fact]
        public void CreatePortfolio_WhenValidParameters_ShouldSucceedAndRaisePortfolioCreatedDomainEvent()
        {
            // Arrange
            var memberId = MemberId.Of(Guid.NewGuid());
            var portfolioName = PortfolioName.Of("name");

            // Act
            var portfolio = Portfolio.Create(memberId, portfolioName);

            // Assert
            using var _ = new AssertionScope();

            var domainEvent = AssertPublishedDomainEvent<PortfolioCreatedDomainEvent>(portfolio);
            domainEvent.PortfolioId.Should().Be(portfolio.Id);
            domainEvent.PortfolioName.Should().Be(portfolioName);
            domainEvent.MemberId.Should().Be(memberId);
        }
    }
}