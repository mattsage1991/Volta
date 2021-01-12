using System.Threading;
using System.Threading.Tasks;

namespace Volta.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEvents(CancellationToken cancellationToken);
    }
}
