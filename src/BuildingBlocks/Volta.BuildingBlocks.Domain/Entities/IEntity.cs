using System.Collections.Generic;
using Volta.BuildingBlocks.Domain.Events;

namespace Volta.BuildingBlocks.Domain.Entities
{
    public interface IEntity
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        void ClearDomainEvents();
    }
}