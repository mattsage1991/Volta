using System.Reflection;
using Autofac;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using Volta.BuildingBlocks.Application.Behaviors;
using Volta.BuildingBlocks.Application.Setup;

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
            
            base.Load(builder);
        }
    }
}
