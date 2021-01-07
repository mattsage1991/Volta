using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Volta.BuildingBlocks.Application;
using Volta.BuildingBlocks.Application.EventBus;
using Volta.BuildingBlocks.Infrastructure.DomainEventsDispatching;

namespace Volta.BuildingBlocks.Infrastructure
{
    public class UnitOfWork<T> : IUnitOfWork
        where T : DbContext
    {
        private readonly DbContext _dbContext;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;
        private readonly IIntegrationEventService _integrationEventService;
        private IDbContextTransaction _dbContextTransaction;
        
        public UnitOfWork(T dbContext, IDomainEventsDispatcher domainEventsDispatcher, IIntegrationEventService integrationEventService)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _domainEventsDispatcher = domainEventsDispatcher ?? throw new ArgumentNullException(nameof(domainEventsDispatcher));
            _integrationEventService = integrationEventService ?? throw new ArgumentNullException(nameof(integrationEventService));
        }

        public IDbContextTransaction GetCurrentTransaction() => _dbContextTransaction;

        public async Task<Guid> BeginTransaction()
        {
            _dbContextTransaction = await _dbContext.Database.BeginTransactionAsync();
            return _dbContextTransaction.TransactionId;
        }

        public async Task Complete(CancellationToken cancellationToken = default)
        {
            await _domainEventsDispatcher.DispatchEvents(cancellationToken);
            
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
                await _dbContextTransaction.CommitAsync(cancellationToken);
                await _integrationEventService.PublishEventsThroughEventBusAsync(_dbContextTransaction.TransactionId);
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_dbContextTransaction != null)
                {
                    _dbContextTransaction.Dispose();
                    _dbContextTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _dbContextTransaction?.Rollback();
            }
            finally
            {
                if (_dbContextTransaction != null)
                {
                    _dbContextTransaction.Dispose();
                    _dbContextTransaction = null;
                }
            }
        }
    }
}