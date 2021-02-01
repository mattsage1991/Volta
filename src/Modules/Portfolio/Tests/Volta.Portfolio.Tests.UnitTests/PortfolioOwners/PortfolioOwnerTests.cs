using System;
using FluentAssertions;
using FluentAssertions.Execution;
using Volta.Portfolios.Domain.PortfolioOwners;
using Volta.Portfolios.Domain.PortfolioOwners.Events;
using Volta.Portfolios.Domain.PortfolioOwners.Rules;
using Volta.Portfolios.Domain.Portfolios.Events;
using Volta.Portfolios.Domain.UnitTests.Portfolios;
using Volta.Portfolios.Tests.UnitTests.SeedWork;
using Xunit;

namespace Volta.Portfolios.Domain.UnitTests.PortfolioOwners
{
    public class PortfolioOwnerTests : TestBase
    {
        private readonly TestHarness harness;

        public PortfolioOwnerTests()
        {
            harness = new TestHarness();
        }

        [Fact]
        public void CreatePortfolioOwner_WhenValidParameters_ShouldSucceedAndRaisePortfolioCreatedDomainEvent()
        {
            // Arrange
            var portfolioOwnerId = PortfolioOwnerId.Of(Guid.NewGuid());
            var portfolioOwnerExistChecker = harness.AlwaysFalsePortfolioOwnerExistsChecker;
            
            // Act
            var portfolioOwner = PortfolioOwner.Create(portfolioOwnerId, portfolioOwnerExistChecker);

            // Assert
            using var _ = new AssertionScope();

            var domainEvent = AssertPublishedDomainEvent<PortfolioOwnerCreatedDomainEvent>(portfolioOwner);
            domainEvent.PortfolioOwnerId.Should().Be(portfolioOwnerId);
            domainEvent.CreatedDate.Should().NotBeNull().And.NotBe(default(DateTime));
        }

        [Fact]
        public void CreatePortfolioOwner_WhenOwnerAlreadyExists_ShouldFailPortfolioOwnerDoesNotAlreadyExistsRule()
        {
            // Arrange
            var portfolioOwnerId = PortfolioOwnerId.Of(Guid.NewGuid());
            var portfolioOwnerExistChecker = harness.AlwaysTruePortfolioOwnerExistsChecker;

            // Act
            Action act = () => PortfolioOwner.Create(portfolioOwnerId, portfolioOwnerExistChecker);

            // Assert
            AssertBrokenRule<PortfolioOwnerDoesNotAlreadyExistRule>(act);
        }
    }
}