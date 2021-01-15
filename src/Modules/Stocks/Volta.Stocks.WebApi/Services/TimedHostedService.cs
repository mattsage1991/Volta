using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Volta.BuildingBlocks.Application;
using Volta.Stocks.Application.Commands.CreateStock;
using Volta.Stocks.Application.Commands.UpdateStocks;

namespace Volta.Stocks.WebApi.Services
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger logger;
        private readonly IRequestExecutor requestExecutor;
        private Timer timer;


        public TimedHostedService(ILogger<TimedHostedService> logger, IRequestExecutor requestExecutor)
        {
            this.logger = logger;
            this.requestExecutor = requestExecutor;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Timed Background Service is starting.");

            timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromDays(1));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            logger.LogInformation("Timed Background Service is working.");
            var numberOfStocksUpdated = await requestExecutor.ExecuteCommand(new UpdateStocksCommand());
            logger.LogInformation($"{numberOfStocksUpdated} stocks have been updated.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Timed Background Service is stopping.");

            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}