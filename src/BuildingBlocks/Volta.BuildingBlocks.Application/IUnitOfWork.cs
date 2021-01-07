using System.Threading;
using System.Threading.Tasks;

namespace Volta.BuildingBlocks.Application
{
    public interface IUnitOfWork
    {
        Task Complete(CancellationToken cancellationToken = default);
    }
}