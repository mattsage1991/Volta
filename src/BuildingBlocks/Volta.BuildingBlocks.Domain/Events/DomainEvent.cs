using System;
using Volta.BuildingBlocks.Domain.Events;

namespace Volta.BuildingBlocks.Domain
{
    public class DomainEvent : IDomainEvent
    {
        public DateTime OccurredOn => DateTime.UtcNow;
    }
}