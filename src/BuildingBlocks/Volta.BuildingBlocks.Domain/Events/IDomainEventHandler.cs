using MediatR;

namespace Volta.BuildingBlocks.Domain.Events
{
    public interface IDomainEventHandler<T> : INotificationHandler<T>
        where T : IDomainEvent
    {
    }
}