using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Portfolios.Domain.PortfolioOwners.Rules
{
    public class PortfolioOwnerDoesNotAlreadyExistRule : IBusinessRule
    {
        private readonly IPortfolioOwnerExistsChecker portfolioOwnerExistsChecker;
        private readonly PortfolioOwnerId portfolioOwnerId;

        public PortfolioOwnerDoesNotAlreadyExistRule(PortfolioOwnerId portfolioOwnerId, IPortfolioOwnerExistsChecker portfolioOwnerExistsChecker)
        {
            this.portfolioOwnerExistsChecker = portfolioOwnerExistsChecker ?? throw new ArgumentNullException(nameof(portfolioOwnerExistsChecker));
            this.portfolioOwnerId = portfolioOwnerId ?? throw new ArgumentNullException(nameof(portfolioOwnerId));
        }

        public bool IsBroken()
        {
            return portfolioOwnerExistsChecker.DoesExist(portfolioOwnerId).GetAwaiter().GetResult();
        }

        public string Message => "The portfolio owner record already exists.";

    }
}