using System;
using Volta.BuildingBlocks.Domain.Entities;
using Volta.BuildingBlocks.Domain.Events;
using Volta.Portfolios.Domain.PortfolioOwners.Events;
using Volta.Portfolios.Domain.PortfolioOwners.Rules;

namespace Volta.Portfolios.Domain.PortfolioOwners
{
    public class PortfolioOwner : Entity<PortfolioOwnerId>, IAggregateRoot
    {
        private CreatedDate createdDate;

        private PortfolioOwner() { }

        private PortfolioOwner(PortfolioOwnerId portfolioOwnerId, IPortfolioOwnerExistsChecker portfolioOwnerExistsChecker)
        {
            CheckRule(new PortfolioOwnerDoesNotAlreadyExistRule(portfolioOwnerId, portfolioOwnerExistsChecker));

            Id = portfolioOwnerId;
            this.createdDate = CreatedDate.Of(DateTime.UtcNow);
            
            AddDomainEvent(new PortfolioOwnerCreatedDomainEvent(this.Id, this.createdDate));
        }

        public static PortfolioOwner Create(PortfolioOwnerId portfolioOwnerId,
            IPortfolioOwnerExistsChecker portfolioOwnerExistsChecker)
        {
            return new PortfolioOwner(portfolioOwnerId, portfolioOwnerExistsChecker);
        }
    }
}