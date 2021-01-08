using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Volta.Stocks.Integration.Core.Common;
using Volta.Stocks.Integration.Core.Common.Interfaces;

namespace Volta.Stocks.Integration.Core
{
    public class TestFixture<TEntryPoint> where TEntryPoint : class
    {
        private readonly AutofacWebApplicationFactory<TEntryPoint> factory;

        private readonly List<ITestAdapter> adapters;

        /// <summary>
        /// A instance of <see cref="HttpClient">HttpClient</see> for use with integration tests.
        /// </summary>
        public HttpClient HttpClient { get; private set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="TestFixture"/> class.
        /// </summary>
        /// <param name="factory">A single <see cref="AutofacWebApplicationFactory{TEntryPoint}"/> reference.</param>
        /// <param name="testHarnesses">A set of <see cref="ITestAdapter"/> test adapters.</param>
        public TestFixture(AutofacWebApplicationFactory<TEntryPoint> factory,
            params ITestAdapter[] testHarnesses)
        {
            this.factory = factory;
            this.adapters = testHarnesses.ToList();
        }

        /// <summary>
        /// Method used to setup and execute a test. Also includes options for data seeding and verification of the persisted data.
        /// </summary>
        /// <param name="seedDefinitions">A list of <see cref="SeedDefinition"/> records.</param>
        /// <param name="test">A single test func to invoke.</param>
        /// <param name="persistenceCheck">A set of checks to verify the persisted data.</param>
        /// <returns></returns>
        public async Task ExecuteAsync(List<SeedDefinition> seedDefinitions,
            Func<Task> test,
            Func<List<ITestChecker>, Task> persistenceCheck = null)
        {
            try
            {
                await SetupAsync();
                await SeedAsync(seedDefinitions);
                await test();
                await CheckAsync(persistenceCheck);
                await TeardownAsync();
            }
            catch (Exception)
            {
                await TeardownAsync();
                throw;
            }
        }

        private Task SetupAsync()
        {
            Task setupTask = null;

            HttpClient = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, configBuilder) =>
                {
                    configBuilder.AddInMemoryCollection(
                       new Dictionary<string, string>
                       {
                       });
                });
                builder.ConfigureTestServices(services =>
                {
                    var setups = GetAdaptersConfirmingTo<ITestSetup>();
                    var setupTasks = setups.Select(x => x.SetupAsync(services)).ToArray();

                    setupTask = Task.WhenAll(setupTasks);
                });
                builder.ConfigureTestContainer<ContainerBuilder>(builders =>
                {
                    var containerSetups = GetAdaptersConfirmingTo<ITestContainer>();
                    var containerSetupTasks = containerSetups.Select(x => x.SetupContainerAsync(builders)).ToArray();

                    setupTask = Task.WhenAll(containerSetupTasks);
                });
            })
            .CreateClient();

            return setupTask;
        }

        private Task SeedAsync(List<SeedDefinition> seedDefinitions)
        {
            if (seedDefinitions != null)
            {
                var seeders = GetAdaptersConfirmingTo<ITestSeeder>();
                var seedTasks = new List<Task>();

                foreach (var definition in seedDefinitions)
                {
                    var adapter = seeders.Single(x => x.SeedType == definition.Type);
                    seedTasks.Add(adapter.SeedAsync(definition.Objects));
                }

                return Task.WhenAll(seedTasks);
            }

            return Task.CompletedTask;
        }

        private Task CheckAsync(Func<List<ITestChecker>, Task> persistenceCheck)
        {
            var checkers = GetAdaptersConfirmingTo<ITestChecker>();

            if (persistenceCheck == null)
            {
                return Task.CompletedTask;
            }

            return persistenceCheck.Invoke(checkers);
        }

        private Task TeardownAsync()
        {
            var teardowns = GetAdaptersConfirmingTo<ITestTeardown>();
            var teardownTasks = teardowns.Select(x => x.TeardownAsync()).ToArray();

            return Task.WhenAll(teardownTasks);
        }

        private List<T> GetAdaptersConfirmingTo<T>() where T : class
        {
            var result = new List<T>();

            foreach (var adapter in adapters)
            {
                var testChecker = adapter as T;

                if (testChecker != null)
                {
                    result.Add(testChecker);
                }
            }

            return result;
        }
    }
}