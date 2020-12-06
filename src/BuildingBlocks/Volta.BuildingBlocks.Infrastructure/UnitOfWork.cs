using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volta.BuildingBlocks.Application;
using Volta.BuildingBlocks.Infrastructure.DomainEventsDispatching;

namespace Volta.BuildingBlocks.Infrastructure
{
    public class UnitOfWork<T> : IUnitOfWork
        where T : DbContext
    {
        private readonly DbContext _dbContext;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        public UnitOfWork(T dbContext, IDomainEventsDispatcher domainEventsDispatcher)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _domainEventsDispatcher = domainEventsDispatcher ?? throw new ArgumentNullException(nameof(domainEventsDispatcher));
        }

        public async Task<int> Complete(CancellationToken cancellationToken = default)
        {
            await _domainEventsDispatcher.DispatchEvents(cancellationToken);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}