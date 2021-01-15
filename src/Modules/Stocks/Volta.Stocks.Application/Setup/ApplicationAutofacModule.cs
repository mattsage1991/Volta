using System.Linq;
using System.Reflection;
using Autofac;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using Volta.BuildingBlocks.Application.Behaviors;
using Volta.BuildingBlocks.Application.Setup;
using Volta.BuildingBlocks.EventBus;

namespace Volta.Stocks.Application.Setup
{
    public class ApplicationAutofacModule : Autofac.Module
    {
        private readonly string _connectionString;

        public ApplicationAutofacModule(string connectionString)
        {
            _connectionString = connectionString ?? throw new System.ArgumentNullException(nameof(connectionString));
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new SqlConnectionString(_connectionString))
                .InstancePerLifetimeScope();

            builder.RegisterModule(new ApplicationBaseAutofacModule());

            builder.RegisterMediatR(Assembly.GetExecutingAssembly());

            builder.RegisterGeneric(typeof(UnitOfWorkTransactionBehavior<,>)).As(typeof(IPipelineBehavior<,>)).InstancePerDependency();

            builder.Register(x => new IntegrationEventTypesProvider(ThisAssembly.GetReferencedAssemblies().Where(x => x.Name.StartsWith("Volta.")).ToArray()))
                .SingleInstance().AsImplementedInterfaces();

            base.Load(builder);
        }
    }
}
