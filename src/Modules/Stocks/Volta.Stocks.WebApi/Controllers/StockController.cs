using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volta.BuildingBlocks.Application;
using Volta.Stocks.Application.Commands.CreateStock;

namespace Volta.Stocks.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IRequestExecutor _requestExecutor;

        public StockController(IRequestExecutor requestExecutor)
        {
            _requestExecutor = requestExecutor ?? throw new ArgumentNullException(nameof(requestExecutor));
        }

        [HttpPost]
        public async Task<Guid> CreateStock()
        {
            return await _requestExecutor.ExecuteCommand(new CreateStockCommand("Tesla", "TSLA"));
        }
    }
}