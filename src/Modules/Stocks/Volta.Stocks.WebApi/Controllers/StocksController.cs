using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volta.BuildingBlocks.Application;
using Volta.Stocks.Application.Commands.CreateStock;
using Volta.Stocks.Application.Models;

namespace Volta.Stocks.WebApi.Controllers
{
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("api/[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly IRequestExecutor requestExecutor;

        public StocksController(IRequestExecutor requestExecutor)
        {
            this.requestExecutor = requestExecutor ?? throw new ArgumentNullException(nameof(requestExecutor));
        }

        [HttpPost]
        public async Task<Guid> CreateStock([FromBody] StockPostModel stockPostModel)
        {
            return await requestExecutor.ExecuteCommand(new CreateStockCommand(stockPostModel.CompanyName, stockPostModel.TickerSymbol));
        }
    }
}