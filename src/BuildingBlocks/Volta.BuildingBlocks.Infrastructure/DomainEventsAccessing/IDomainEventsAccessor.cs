using System.Collections.Generic;
using Volta.BuildingBlocks.Domain.Events;

namespace Volta.BuildingBlocks.Infrastructure.DomainEventsAccessing
{
    public interface IDomainEventsAccessor
    {
        List<IDomainEvent> GetAllDomainEvents();

        void ClearAllDomainEvents();
    }
}