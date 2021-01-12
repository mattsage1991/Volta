using System;
using Autofac;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

namespace Volta.BuildingBlocks.EventBus.AzureServiceBus.Setup
{
    public class AzureServiceBusAutofacModule : Autofac.Module
    {
        private readonly string eventBusConnection;
        private readonly string subscriptionClientName;

        public AzureServiceBusAutofacModule(string eventBusConnection, string subscriptionClientName)
        {
            this.eventBusConnection = eventBusConnection ?? throw new ArgumentNullException(nameof(eventBusConnection));
            this.subscriptionClientName = subscriptionClientName ?? throw new ArgumentNullException(nameof(subscriptionClientName));
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var eventBusSubscriptionsManager = c.Resolve<IEventBusSubscriptionsManager>();

                var serviceBusConnectionString = eventBusConnection;
                var serviceBusConnection = new ServiceBusConnectionStringBuilder(serviceBusConnectionString);

                return new DefaultServiceBusPersisterConnection(serviceBusConnection);
            }).AsImplementedInterfaces().AsSelf().SingleInstance();


            builder.Register(c =>
            {
                var serviceBusPersisterConnection = c.Resolve<IServiceBusPersisterConnection>();
                var iLifetimeScope = c.Resolve<ILifetimeScope>();
                var logger = c.Resolve<ILogger<EventBusServiceBus>>();
                var eventBusSubscriptionManager = c.Resolve<IEventBusSubscriptionsManager>();

                return new EventBusServiceBus(serviceBusPersisterConnection, logger,
                    eventBusSubscriptionManager, subscriptionClientName, iLifetimeScope);
            }).AsImplementedInterfaces().AsSelf().SingleInstance();

            base.Load(builder);
        }
    }
}
