using System;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Volta.BuildingBlocks.Application;
using Volta.BuildingBlocks.EventBus.Abstractions;
using Volta.BuildingBlocks.EventBus.Events;
using Volta.BuildingBlocks.EventBus.IntegrationEventLog.Services;

namespace Volta.BuildingBlocks.Infrastructure.EntityFramework.IntegrationEventServices
{
    public class IntegrationEventPublisher<TDbContext> : IIntegrationEventPublisher
        where TDbContext : DbContext
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory;
        private readonly ICurrentTransactionAccessor currentTransactionAccessor;
        private readonly TDbContext dbContext;
        private readonly IIntegrationEventLogService eventLogService;
        private readonly ILogger<IntegrationEventPublisher<TDbContext>> logger;

        public IntegrationEventPublisher(IEventBus eventBus,
            TDbContext dbContext,
            IntegrationEventLogContext eventLogContext,
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory,
            ICurrentTransactionAccessor currentTransactionAccessor,
            ILogger<IntegrationEventPublisher<TDbContext>> logger)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            this.currentTransactionAccessor = currentTransactionAccessor ?? throw new ArgumentNullException(nameof(currentTransactionAccessor));
            eventLogService = this.integrationEventLogServiceFactory(this.dbContext.Database.GetDbConnection());
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task Publish(IntegrationEvent evt)
        {
            logger.LogInformation("----- Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", evt.Id, evt);

            await eventLogService.SaveEvent(evt, currentTransactionAccessor.GetCurrentTransaction());
        }
    }
}
