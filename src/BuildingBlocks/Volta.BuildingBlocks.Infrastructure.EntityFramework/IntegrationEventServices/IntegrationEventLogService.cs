using System;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Volta.BuildingBlocks.EventBus;
using Volta.BuildingBlocks.EventBus.Events;
using Volta.BuildingBlocks.EventBus.IntegrationEventLog;
using Volta.BuildingBlocks.EventBus.IntegrationEventLog.Services;

namespace Volta.BuildingBlocks.Infrastructure.EntityFramework.IntegrationEventServices
{
    public class IntegrationEventLogService : IIntegrationEventLogService, IDisposable
    {
        private readonly IntegrationEventLogContext integrationEventLogContext;
        private readonly DbConnection dbConnection;
        private volatile bool disposedValue;

        public IntegrationEventLogService(IIntegrationEventTypesProvider integrationEventsProvider, DbConnection dbConnection)
        {
            this.dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            integrationEventLogContext = new IntegrationEventLogContext(
                new DbContextOptionsBuilder<IntegrationEventLogContext>()
                    .UseSqlServer(this.dbConnection)
                    .Options);
        }

        public Task SaveEvent(IntegrationEvent @event, object transaction)
        {
            var transaction2 = transaction as IDbContextTransaction;
            if (transaction2 == null) throw new ArgumentNullException(nameof(transaction));

            var eventLogEntry = new IntegrationEventLogEntry(@event, transaction2.TransactionId);

            integrationEventLogContext.Database.UseTransaction(transaction2.GetDbTransaction());
            integrationEventLogContext.IntegrationEventLogs.Add(eventLogEntry);

            return integrationEventLogContext.SaveChangesAsync();
        }
        

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    integrationEventLogContext?.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
