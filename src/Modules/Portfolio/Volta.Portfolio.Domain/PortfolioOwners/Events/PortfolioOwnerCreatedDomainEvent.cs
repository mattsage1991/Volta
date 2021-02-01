using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Portfolios.Domain.PortfolioOwners.Events
{
    public class PortfolioOwnerCreatedDomainEvent : DomainEvent
    {
        public PortfolioOwnerId PortfolioOwnerId { get; }
        public CreatedDate CreatedDate { get; }

        public PortfolioOwnerCreatedDomainEvent(PortfolioOwnerId portfolioOwnerId, CreatedDate createdDate)
        {
            PortfolioOwnerId = portfolioOwnerId;
            CreatedDate = createdDate;
        }
    }
}