using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volta.Stocks.Domain.Stocks;

namespace Volta.Stocks.Infrastructure.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly StocksContext dbContext;

        public StockRepository(StocksContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task Add(Stock stock, CancellationToken cancellationToken = default)
        {
            await dbContext.Stocks.AddAsync(stock, cancellationToken);
        }

        public async Task<Stock> GetById(StockId id, CancellationToken cancellationToken = default)
        {
            return await dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}