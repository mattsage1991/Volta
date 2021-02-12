using System.Reflection;
using Autofac;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using Volta.BuildingBlocks.Application.Behaviors;
using Volta.BuildingBlocks.Application.Setup;

namespace Volta.UserAccess.Application.Setup
{
    public class ApplicationAutofacModule : Autofac.Module
    {
        private readonly string connectionString;

        public ApplicationAutofacModule(string connectionString)
        {
            this.connectionString = connectionString ?? throw new System.ArgumentNullException(nameof(connectionString));
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new SqlConnectionString(connectionString))
                .InstancePerLifetimeScope();

            builder.RegisterModule(new ApplicationBaseAutofacModule());

            builder.RegisterMediatR(Assembly.GetExecutingAssembly());

            builder.RegisterGeneric(typeof(UnitOfWorkTransactionBehavior<,>)).As(typeof(IPipelineBehavior<,>)).InstancePerDependency();

            //builder.Register(x => new IntegrationEventTypesProvider(ThisAssembly.GetReferencedAssemblies().Where(x => x.Name.StartsWith("Volta.")).ToArray()))
            //    .SingleInstance().AsImplementedInterfaces();

            base.Load(builder);
        }
    }
}