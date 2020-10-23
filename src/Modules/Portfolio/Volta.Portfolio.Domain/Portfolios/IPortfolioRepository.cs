using System.Threading.Tasks;

namespace Volta.Portfolios.Domain.Portfolios
{
    public interface IPortfolioRepository
    {
        Task Add(Portfolio portfolio);

        Task<Portfolio> GetById(PortfolioId id);
    }
}