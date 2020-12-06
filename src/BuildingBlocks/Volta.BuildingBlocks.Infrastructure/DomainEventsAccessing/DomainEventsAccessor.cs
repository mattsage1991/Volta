using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volta.BuildingBlocks.Domain;
using Volta.BuildingBlocks.Domain.Entities;
using Volta.BuildingBlocks.Domain.Events;
using Volta.BuildingBlocks.Infrastructure.DomainEventsDispatching;

namespace Volta.BuildingBlocks.Infrastructure.DomainEventsAccessing
{
    public class DomainEventsAccessor<T> : IDomainEventsAccessor
        where T : DbContext
    {
        private readonly DbContext _dbContext;

        public DomainEventsAccessor(T dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); ;
        }

        public List<IDomainEvent> GetAllDomainEvents()
        {
            var domainEntities = _dbContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            return domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();
        }

        public void ClearAllDomainEvents()
        {
            var domainEntities = _dbContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            domainEntities
                .ForEach(entity => entity.Entity.ClearDomainEvents());
        }
    }
}