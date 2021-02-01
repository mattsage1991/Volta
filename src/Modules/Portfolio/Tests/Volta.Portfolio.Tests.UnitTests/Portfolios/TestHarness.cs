using System;
using System.Threading.Tasks;
using Moq;
using Volta.Portfolios.Domain.PortfolioOwners;
using Volta.Portfolios.Domain.Portfolios;

namespace Volta.Portfolios.Domain.UnitTests.Portfolios
{
    public class TestHarness
    {
        public Portfolio CreatePortfolio()
        {
            var portfolio = Portfolio.Create(PortfolioOwnerId.Of(Guid.NewGuid()), PortfolioName.Of("name"));
            return portfolio;
        }

        public IPortfolioOwnerExistsChecker AlwaysFalsePortfolioOwnerExistsChecker
        {
            get
            {
                var stockExistsChecker = new Mock<IPortfolioOwnerExistsChecker>();
                stockExistsChecker.Setup(x => x.DoesExist(It.IsAny<PortfolioOwnerId>())).Returns(Task.FromResult(false));
                return stockExistsChecker.Object;
            }
        }

        public IPortfolioOwnerExistsChecker AlwaysTruePortfolioOwnerExistsChecker
        {
            get
            {
                var stockExistsChecker = new Mock<IPortfolioOwnerExistsChecker>();
                stockExistsChecker.Setup(x => x.DoesExist(It.IsAny<PortfolioOwnerId>())).Returns(Task.FromResult(true));
                return stockExistsChecker.Object;
            }
        }
    }
}