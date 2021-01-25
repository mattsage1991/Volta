using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Portfolios.Domain.Portfolios.Events
{
    public class PortfolioCreatedDomainEvent : DomainEvent
    {
        public PortfolioId PortfolioId { get; }
        public MemberId MemberId { get; }
        public PortfolioName PortfolioName { get; }
        public DateTime CreatedDate { get; }
        
        public PortfolioCreatedDomainEvent(PortfolioId portfolioId, MemberId memberId, PortfolioName portfolioName, DateTime createdDate)
        {
            PortfolioId = portfolioId;
            MemberId = memberId;
            PortfolioName = portfolioName;
            CreatedDate = createdDate;
        }
    }
}