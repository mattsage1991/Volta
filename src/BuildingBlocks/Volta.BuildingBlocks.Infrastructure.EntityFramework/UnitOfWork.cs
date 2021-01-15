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
        private readonly ICurrentTransactionAccessor currentTransactionAccessor;
        private readonly IDomainEventsDispatcher domainEventsDispatcher;
        private readonly IInternalIntegrationEventPublisher internalIntegrationEventPublisher;
        private IDbContextTransaction dbContextTransaction;

        public UnitOfWork(T dbContext, ICurrentTransactionAccessor currentTransactionAccessor,
            IDomainEventsDispatcher domainEventsDispatcher,
            IInternalIntegrationEventPublisher internalIntegrationEventPublisher)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.currentTransactionAccessor = currentTransactionAccessor ?? throw new ArgumentNullException(nameof(currentTransactionAccessor));
            this.domainEventsDispatcher = domainEventsDispatcher ?? throw new ArgumentNullException(nameof(domainEventsDispatcher));
            this.internalIntegrationEventPublisher = internalIntegrationEventPublisher ?? throw new ArgumentNullException(nameof(internalIntegrationEventPublisher));
        }


        public async Task<Guid> BeginTransaction()
        {
            this.dbContextTransaction = await dbContext.Database.BeginTransactionAsync();
            currentTransactionAccessor.SetCurrentTransaction(dbContextTransaction);
            return dbContextTransaction.TransactionId;
        }



        public async Task Complete(CancellationToken cancellationToken = default)
        {
            await domainEventsDispatcher.DispatchEvents(cancellationToken);
            try
            {
                await dbContext.SaveChangesAsync();
                await dbContextTransaction.CommitAsync();
                await internalIntegrationEventPublisher.PublishEventsThroughEventBusByTransactionId(dbContextTransaction.TransactionId);
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (dbContextTransaction != null)
                {
                    dbContextTransaction.Dispose();
                    dbContextTransaction = null;
                }
            }
        }

        public object GetCurrentTransaction()
        {
            return dbContextTransaction;
        }

        public void RollbackTransaction()
        {
            try
            {
                dbContextTransaction?.Rollback();
            }
            finally
            {
                if (dbContextTransaction != null)
                {
                    dbContextTransaction.Dispose();
                    dbContextTransaction = null;
                }
            }
        }
    }
}
