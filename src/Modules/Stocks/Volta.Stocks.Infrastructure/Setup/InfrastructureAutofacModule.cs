using System;
using System.Net.Http;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Volta.BuildingBlocks.EventBus;
using Volta.BuildingBlocks.Infrastructure.EntityFramework;
using Volta.BuildingBlocks.Infrastructure.EntityFramework.Setup;
using Volta.Stocks.Infrastructure.Services;

namespace Volta.Stocks.Infrastructure.Setup
{
    public class InfrastructureAutofacModule : Autofac.Module
    {
        private readonly string databaseConnectionString;

        public InfrastructureAutofacModule(string databaseConnectionString)
        {
            this.databaseConnectionString = databaseConnectionString ?? throw new ArgumentNullException(nameof(databaseConnectionString));
        }

        /// <summary>
        /// The Load.
        /// </summary>
        /// <param name="builder">The builder <see cref="ContainerBuilder"/>.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var opt = new DbContextOptionsBuilder<StocksContext>()
                    .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();
                opt.UseSqlServer(databaseConnectionString);

                return new StocksContext(opt.Options);
            }).AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<InMemoryEventBusSubscriptionsManager>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterModule(new InfrastructureEntityFrameworkAutofacModule<StocksContext>(databaseConnectionString));

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<IEXCloudService>().AsImplementedInterfaces().SingleInstance().WithParameter(
                (p, ctx) => p.ParameterType == typeof(HttpClient),
                (p, ctx) => ctx.Resolve<IHttpClientFactory>().CreateClient());
            
            base.Load(builder);
        }
    }
}