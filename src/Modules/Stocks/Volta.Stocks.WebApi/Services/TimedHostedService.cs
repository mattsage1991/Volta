using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Volta.BuildingBlocks.Application;
using Volta.Stocks.Application.Commands.CreateStock;

namespace Volta.Stocks.WebApi.Services
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IRequestExecutor _requestExecutor;
        private Timer _timer;


        public TimedHostedService(ILogger<TimedHostedService> logger, IRequestExecutor requestExecutor)
        {
            _logger = logger;
            _requestExecutor = requestExecutor;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromDays(1));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            _logger.LogInformation("Timed Background Service is working.");
            var companyName = "Tesla";
            var symbol = "TSLA";
            var stockId = await _requestExecutor.ExecuteCommand(new CreateStockCommand(companyName, symbol));
            _logger.LogInformation($"Stock Created Successfully: {companyName}:{symbol} with StockId {stockId}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}