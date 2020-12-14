using System;
using System.Net.Http;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Volta.BuildingBlocks.Infrastructure.Setup;
using Volta.Stocks.Infrastructure.Services;

namespace Volta.Stocks.Infrastructure.Setup
{
    public class InfrastructureAutofacModule : Autofac.Module
    {
        private readonly string _databaseConnectionString;

        public InfrastructureAutofacModule(string databaseConnectionString)
        {
            _databaseConnectionString = databaseConnectionString ?? throw new ArgumentNullException(nameof(databaseConnectionString));
        }

        /// <summary>
        /// The Load.
        /// </summary>
        /// <param name="builder">The builder <see cref="ContainerBuilder"/>.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new InfrastructureBaseAutofacModule<StockDbContext>());

            builder.Register(c =>
            {
                var opt = new DbContextOptionsBuilder<StockDbContext>();
                opt.UseSqlServer(_databaseConnectionString);

                return new StockDbContext(opt.Options);
            }).AsSelf().InstancePerLifetimeScope();

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