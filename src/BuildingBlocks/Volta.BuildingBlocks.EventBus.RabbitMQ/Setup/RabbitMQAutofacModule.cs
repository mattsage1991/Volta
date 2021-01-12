using System;
using Autofac;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Volta.BuildingBlocks.EventBus.RabbitMQ.Setup
{
    public class RabbitMQAutofacModule : Autofac.Module
    {
        private readonly string eventBusRetryCount;
        private readonly string subscriptionClientName;
        private readonly string eventBusConnection;
        private readonly string eventBusUserName;
        private readonly string eventBusPassword;

        public RabbitMQAutofacModule(string eventBusRetryCount, string subscriptionClientName,
            string eventBusConnection, string eventBusUserName, string eventBusPassword)
        {
            this.eventBusRetryCount = eventBusRetryCount;
            this.subscriptionClientName = subscriptionClientName ?? throw new ArgumentNullException(nameof(subscriptionClientName));
            this.eventBusConnection = eventBusConnection ?? throw new ArgumentNullException(nameof(eventBusConnection));
            this.eventBusUserName = eventBusUserName;
            this.eventBusPassword = eventBusPassword;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var rabbitMQPersistentConnection = c.Resolve<IRabbitMQPersistentConnection>();
                var iLifetimeScope = c.Resolve<ILifetimeScope>();
                var logger = c.Resolve<ILogger<EventBusRabbitMQ>>();
                var eventBusSubscriptionsManager = c.Resolve<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(eventBusRetryCount))
                {
                    retryCount = int.Parse(eventBusRetryCount);
                }

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope,
                    eventBusSubscriptionsManager, subscriptionClientName, retryCount);
            }).AsImplementedInterfaces().AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var logger = c.Resolve<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = eventBusConnection,
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(eventBusUserName))
                {
                    factory.UserName = eventBusUserName;
                }

                if (!string.IsNullOrEmpty(eventBusPassword))
                {
                    factory.Password = eventBusPassword;
                }

                var retryCount = 5;
                if (!string.IsNullOrEmpty(eventBusRetryCount))
                {
                    retryCount = int.Parse(eventBusRetryCount);
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            }).AsImplementedInterfaces().AsSelf().SingleInstance();

            base.Load(builder);
        }
    }
}
