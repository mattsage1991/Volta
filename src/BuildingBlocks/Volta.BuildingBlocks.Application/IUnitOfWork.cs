using System.Threading;
using System.Threading.Tasks;

namespace Volta.BuildingBlocks.Application
{
    public interface IUnitOfWork
    {
        Task<int> Complete(CancellationToken cancellationToken = default);
    }
}