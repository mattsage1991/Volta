using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Volta.Stocks.WebApi.IntegrationTests.Controllers
{
    [Collection("ControllerTests")]
    public class BaseControllerTests
    {
        protected IConfigurationRoot Configuration;
        protected string ConnectionString;

        public BaseControllerTests()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            this.Configuration = config;

            this.ConnectionString = this.Configuration.GetConnectionString("Database");
        }

        /// <summary>
        /// A method to form a new <see cref="HttpRequestOptions"/> including authorisation headers.
        /// </summary>
        /// <param name="uri">A single string uri value.</param>
        /// <param name="httpMethod">A single <see cref="HttpMethod"/> type reference.</param>
        /// <param name="httpContent">A single <see cref="HttpContent"/> content reference.</param>
        /// <param name="claims">A list of <see cref="Claim"/> claims.</param>
        /// <returns></returns>
        protected HttpRequestMessage GenerateAuthenticatedRequestMessage(string uri, HttpMethod httpMethod, HttpContent httpContent = null, List<Claim> claims = null)
        {
            // TODO - Add in claims, Headers etc for auth as required.

            var requestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(uri),
                Method = httpMethod,
                Headers = {
                }
            };

            if (httpContent != null)
            {
                requestMessage.Content = httpContent;
            }

            return requestMessage;
        }
    }
}