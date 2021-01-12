using System;
using Volta.Stocks.Infrastructure;
using Volta.Stocks.Integration.Core;
using Volta.Stocks.Integration.Core.Infrastructure.EFCore.SqlServer;

namespace Volta.Stocks.WebApi.IntegrationTests
{
    public class DefaultTestFixture : TestFixture<Startup>
    {
        public DefaultTestFixture(Guid testRunId, string connectionString, AutofacWebApplicationFactory<Startup> factory)
            : base(factory,
                new EFCoreSqlServerTestAdapter<StocksContext>(connectionString)
            )
        {
        }

        public static DefaultTestFixture Create(Guid testRunId, string connectionString, AutofacWebApplicationFactory<Startup> factory)
        {
            return new DefaultTestFixture(testRunId, connectionString, factory);
        }
    }
}