using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volta.Portfolios.Domain.Portfolios;

namespace Volta.Portfolios.Infrastructure.Domain.Portfolios
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly PortfolioContext context;

        public PortfolioRepository(PortfolioContext context)
        {
            this.context = context;
        }

        public async Task Add(Portfolio portfolio)
        {
            await context.Portfolios.AddAsync(portfolio);
        }

        public async Task<Portfolio> GetById(PortfolioId id)
        {
            return await context.Portfolios.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}