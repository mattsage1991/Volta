using System;
using Volta.BuildingBlocks.Domain;
using Volta.Portfolios.Domain.PortfolioOwners;

namespace Volta.Portfolios.Domain.Portfolios.Events
{
    public class PortfolioCreatedDomainEvent : DomainEvent
    {
        public PortfolioId PortfolioId { get; }
        public PortfolioOwnerId PortfolioOwnerId { get; }
        public PortfolioName PortfolioName { get; }
        public PortfolioCreatedDate CreatedDate { get; }
        
        public PortfolioCreatedDomainEvent(PortfolioId portfolioId, PortfolioOwnerId portfolioOwnerId, PortfolioName portfolioName, PortfolioCreatedDate createdDate)
        {
            PortfolioId = portfolioId;
            PortfolioOwnerId = portfolioOwnerId;
            PortfolioName = portfolioName;
            CreatedDate = createdDate;
        }
    }
}