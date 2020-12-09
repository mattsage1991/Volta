using System;
using System.Threading;
using System.Threading.Tasks;
using Volta.Stocks.Domain.Stocks;

namespace Volta.Stocks.Infrastructure.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly StockDbContext _dbContext;

        public StockRepository(StockDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task Add(Stock stock, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(stock, cancellationToken);
        }
    }
}