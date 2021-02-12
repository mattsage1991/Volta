using System;
using System.Net.Http;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Volta.BuildingBlocks.EventBus;
using Volta.BuildingBlocks.Infrastructure.EntityFramework;
using Volta.BuildingBlocks.Infrastructure.EntityFramework.Setup;
using Volta.UserAccess.Infrastructure.Password;

namespace Volta.UserAccess.Infrastructure.Setup
{
    public class InfrastructureAutofacModule : Autofac.Module
    {
        private readonly string databaseConnectionString;

        public InfrastructureAutofacModule(string databaseConnectionString)
        {
            this.databaseConnectionString = databaseConnectionString ??
                                            throw new ArgumentNullException(nameof(databaseConnectionString));
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var opt = new DbContextOptionsBuilder<UsersContext>()
                    .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();
                opt.UseSqlServer(databaseConnectionString);

                return new UsersContext(opt.Options);
            }).AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<InMemoryEventBusSubscriptionsManager>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterModule(
                new InfrastructureEntityFrameworkAutofacModule<UsersContext>(databaseConnectionString));

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            
            builder.RegisterType<PasswordHasher>().AsImplementedInterfaces().InstancePerDependency();

            base.Load(builder);
        }
    }
}