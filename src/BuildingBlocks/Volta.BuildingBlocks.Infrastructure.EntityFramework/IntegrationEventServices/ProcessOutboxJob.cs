using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Logging;
using Quartz;
using Volta.BuildingBlocks.EventBus;
using Volta.BuildingBlocks.EventBus.Abstractions;
using Volta.BuildingBlocks.EventBus.IntegrationEventLog.Services;

namespace Volta.BuildingBlocks.Infrastructure.EntityFramework.IntegrationEventServices
{
    [DisallowConcurrentExecution]
    public class ProcessOutboxJob : IJob
    {
        private readonly List<Type> integrationEventTypes;
        private readonly ILifetimeScope lifeTimeScope;
        private readonly ILogger<ProcessOutboxJob> logger;

        public ProcessOutboxJob(ILifetimeScope lifeTimeScope, IIntegrationEventTypesProvider integrationEventsProvider, ILogger<ProcessOutboxJob> logger)
        {
            if (integrationEventsProvider is null)
            {
                throw new ArgumentNullException(nameof(integrationEventsProvider));
            }

            integrationEventTypes = integrationEventsProvider.GetIntegrationEvents();
            this.lifeTimeScope = lifeTimeScope ?? throw new ArgumentNullException(nameof(lifeTimeScope));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using var scope = lifeTimeScope.BeginLifetimeScope();

            var dbContext = scope.Resolve<IntegrationEventLogContext>();
            var eventBus = scope.Resolve<IEventBus>();
            var internalIntegrationEventPublisher = scope.Resolve<IInternalIntegrationEventPublisher>();

            logger.LogInformation("----- Outbox started");

            await internalIntegrationEventPublisher.PublishEventsThroughEventsBus();            

            logger.LogInformation("----- Outbox finished");
        }
    }
}
