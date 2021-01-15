using System;
using System.Threading;
using System.Threading.Tasks;

namespace Volta.BuildingBlocks.Application
{
    public interface IUnitOfWork
    {
        Task<Guid> BeginTransaction();
        object GetCurrentTransaction();
        Task Complete(CancellationToken cancellationToken = default);

    }
}