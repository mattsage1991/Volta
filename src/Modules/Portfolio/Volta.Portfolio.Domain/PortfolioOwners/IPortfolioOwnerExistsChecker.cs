using System.Threading.Tasks;

namespace Volta.Portfolios.Domain.PortfolioOwners
{
    public interface IPortfolioOwnerExistsChecker
    {
        Task<bool> DoesExist(PortfolioOwnerId portfolioOwnerId);

    }
}