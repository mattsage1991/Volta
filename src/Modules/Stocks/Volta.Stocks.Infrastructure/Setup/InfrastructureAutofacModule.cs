using System;
using System.Net.Http;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Volta.BuildingBlocks.Application.EventBus;
using Volta.BuildingBlocks.Infrastructure.EventBus;
using Volta.BuildingBlocks.Infrastructure.IntegrationEventLog;
using Volta.BuildingBlocks.Infrastructure.RabbitMQ;
using Volta.BuildingBlocks.Infrastructure.Setup;
using Volta.Stocks.Infrastructure.Services;

namespace Volta.Stocks.Infrastructure.Setup
{
    public class InfrastructureAutofacModule : Autofac.Module
    {
        private readonly string _databaseConnectionString;
        private readonly string _eventBusRetryCount;
        private readonly string _subscriptionClientName;
        private readonly string _eventBusConnection;
        private readonly string _eventBusUserName;
        private readonly string _eventBusPassword;

        public InfrastructureAutofacModule(string databaseConnectionString, string eventBusRetryCount, string subscriptionClientName, 
            string eventBusConnection, string eventBusUserName, string eventBusPassword)
        {
            _databaseConnectionString = databaseConnectionString ?? throw new ArgumentNullException(nameof(databaseConnectionString));
            _eventBusRetryCount = eventBusRetryCount ?? throw new ArgumentNullException(nameof(eventBusRetryCount));
            _subscriptionClientName = subscriptionClientName ?? throw new ArgumentNullException(nameof(subscriptionClientName));
            _eventBusConnection = eventBusConnection ?? throw new ArgumentNullException(nameof(eventBusConnection));
            _eventBusUserName = eventBusUserName ?? throw new ArgumentNullException(nameof(eventBusUserName));
            _eventBusPassword = eventBusPassword ?? throw new ArgumentNullException(nameof(eventBusPassword));
        }

        /// <summary>
        /// The Load.
        /// </summary>
        /// <param name="builder">The builder <see cref="ContainerBuilder"/>.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new InfrastructureBaseAutofacModule<StocksContext>());

            builder.Register(c =>
            {
                var opt = new DbContextOptionsBuilder<StocksContext>();
                opt.UseSqlServer(_databaseConnectionString);

                return new StocksContext(opt.Options);
            }).AsSelf().InstancePerLifetimeScope();

            builder.Register(c =>
            {
                var opt = new DbContextOptionsBuilder<IntegrationEventLogContext>();
                opt.UseSqlServer(_databaseConnectionString);

                return new IntegrationEventLogContext(opt.Options);
            }).AsSelf().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<IEXCloudService>().AsImplementedInterfaces().SingleInstance().WithParameter(
                (p, ctx) => p.ParameterType == typeof(HttpClient),
                (p, ctx) => ctx.Resolve<IHttpClientFactory>().CreateClient());

            builder.Register(c =>
            {
                var rabbitMQPersistentConnection = c.Resolve<IRabbitMQPersistentConnection>();
                var iLifetimeScope = c.Resolve<ILifetimeScope>();
                var logger = c.Resolve<ILogger<EventBusRabbitMQ>>();
                var eventBusSubscriptionsManager = c.Resolve<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(_eventBusRetryCount))
                {
                    retryCount = int.Parse(_eventBusRetryCount);
                }

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope,
                    eventBusSubscriptionsManager, _subscriptionClientName, retryCount);
            }).AsImplementedInterfaces().AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var logger = c.Resolve<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = _eventBusConnection,
                    DispatchConsumersAsync = true,
                    UserName = _eventBusUserName,
                    Password = _eventBusPassword
                };

                var retryCount = 5;
                if (!string.IsNullOrEmpty(_eventBusRetryCount))
                {
                    retryCount = int.Parse(_eventBusRetryCount);
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            }).AsImplementedInterfaces().AsSelf().SingleInstance();

            base.Load(builder);
        }
    }
}