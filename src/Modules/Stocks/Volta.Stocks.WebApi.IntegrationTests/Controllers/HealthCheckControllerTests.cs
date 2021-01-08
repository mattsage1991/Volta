using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Volta.Stocks.Integration.Core;
using Xunit;

namespace Volta.Stocks.WebApi.IntegrationTests.Controllers
{
    public class HealthCheckControllerTests : BaseControllerTests, IClassFixture<AutofacWebApplicationFactory<Startup>>
    {
        private readonly AutofacWebApplicationFactory<Startup> factory;

        public HealthCheckControllerTests(AutofacWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task When_WebApiHealthCheckIsAvailable_HttpRequestReturnsOk()
        {
            // Arrange
            var testFixture = DefaultTestFixture.Create(Guid.NewGuid(), this.ConnectionString, this.factory);

            // Act
            // Assert
            await testFixture.ExecuteAsync(null, async () =>
            {
                var request = this.GenerateAuthenticatedRequestMessage(
                    $"{testFixture.HttpClient.BaseAddress.ToString()}healthcheck",
                    HttpMethod.Get,
                    claims: null
                );

                var response = await testFixture.HttpClient.SendAsync(request).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                responseBody.Should().NotBeNullOrEmpty().And.Be("Healthy");
            }).ConfigureAwait(false);
        }
    }
}