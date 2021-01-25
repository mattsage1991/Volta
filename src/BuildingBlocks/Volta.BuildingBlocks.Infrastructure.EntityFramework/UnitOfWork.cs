using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Volta.BuildingBlocks.Application;
using Volta.BuildingBlocks.EventBus.IntegrationEventLog.Services;
using Volta.BuildingBlocks.Infrastructure.DomainEventsDispatching;

namespace Volta.BuildingBlocks.Infrastructure.EntityFramework
{
    public class UnitOfWork<T> : IUnitOfWork
        where T : DbContext
    {
        private readonly DbContext dbContext;
        private readonly IDomainEventsDispatcher domainEventsDispatcher;

        public UnitOfWork(T dbContext, IDomainEventsDispatcher domainEventsDispatcher)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.domainEventsDispatcher = domainEventsDispatcher ?? throw new ArgumentNullException(nameof(domainEventsDispatcher));
        }

        public async Task<int> Complete(CancellationToken cancellationToken = default)
        {
            await domainEventsDispatcher.DispatchEvents(cancellationToken);
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
