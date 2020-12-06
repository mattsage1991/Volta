using System.Threading;
using System.Threading.Tasks;

namespace Volta.Stocks.Domain.Stocks
{
    public interface IStockRepository
    {
        Task Add(Stock stock, CancellationToken cancellationToken = default);
    }
}