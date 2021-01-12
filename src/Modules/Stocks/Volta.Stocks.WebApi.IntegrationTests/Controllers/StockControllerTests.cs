using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Volta.Stocks.Application.Models;
using Volta.Stocks.Domain.Stocks;
using Volta.Stocks.Infrastructure;
using Volta.Stocks.Integration.Core;
using Volta.Stocks.Integration.Core.Infrastructure.EFCore;
using Xunit;

namespace Volta.Stocks.WebApi.IntegrationTests.Controllers
{
    public class StockControllerTests : BaseControllerTests, IClassFixture<AutofacWebApplicationFactory<Startup>>
    {
        private readonly AutofacWebApplicationFactory<Startup> factory;

        public StockControllerTests(AutofacWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task When_CreateNewStockWithValidParameters_HttpRequestShouldReturnSuccessStatusCode()
        {
            // Arrange
            var testFixture = DefaultTestFixture.Create(Guid.NewGuid(), this.ConnectionString, this.factory);

            var request = new StockPostModel
            {
                CompanyName = "Tesla",
                TickerSymbol = "TSLA"
            };

            // Assert
            await testFixture.ExecuteAsync(null, async () =>
            {
                var requestBody = JsonConvert.SerializeObject(request);

                var httpRequest = this.GenerateAuthenticatedRequestMessage(
                    $"{testFixture.HttpClient.BaseAddress.ToString()}api/Stocks",
                    HttpMethod.Post,
                    new StringContent(requestBody, Encoding.UTF8, "application/json"),
                    claims: null
                );

                // Perform the call to the API endpoint.
                var response = await testFixture.HttpClient.SendAsync(httpRequest).ConfigureAwait(false);

                // Assert
                response.EnsureSuccessStatusCode();

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
            }, async (checkers) =>
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
            {
                var context = (StocksContext)checkers.Single(x => x.CheckType == EFCoreTestCheckType.Instance).GetCheckObject();

                // Verify the data written to the datastore is correct.
                var sqlQuery = $"SELECT * FROM [Volta.Stocks].Stocks WHERE CompanyName = '{request.CompanyName}' " +
                               $"AND TickerSymbol = '{request.TickerSymbol}' ";

                var customer1 = await context.Stocks.FromSqlRaw(sqlQuery).FirstOrDefaultAsync().ConfigureAwait(false);
                customer1.Should().NotBeNull().And.BeAssignableTo<Stock>();
                customer1.Id.Value.Should().NotBeEmpty().And.NotBe(default(Guid));

            }).ConfigureAwait(false);
        }


    }
}