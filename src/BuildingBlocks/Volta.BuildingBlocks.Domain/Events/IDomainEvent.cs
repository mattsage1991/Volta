using System;
using MediatR;

namespace Volta.BuildingBlocks.Domain.Events
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}
