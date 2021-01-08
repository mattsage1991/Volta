using System;
using Autofac;
using Volta.Stocks.Application.Setup;

namespace Volta.Stocks.Integration.Core.Setup
{
    public class TestApplicationAutofacModule : Autofac.Module
    {
        private readonly string databaseConnectionString;

        public TestApplicationAutofacModule(string databaseConnectionString)
        {
            this.databaseConnectionString = databaseConnectionString ?? throw new ArgumentNullException(nameof(databaseConnectionString));
        }

        /// <summary>
        /// Application Module to add overrides for use with integration tests.
        /// </summary>
        /// <param name="builder">The builder <see cref="ContainerBuilder"/>.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new SqlConnectionString(this.databaseConnectionString))
                .InstancePerLifetimeScope();
        }
    }
}