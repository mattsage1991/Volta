using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volta.BuildingBlocks.Application;
using Volta.Portfolio.API.Controllers.Requests;
using Volta.Portfolios.Application.Commands.CreatePortfolios;

namespace Volta.Portfolio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IRequestExecutor requestExecutor;

        public PortfolioController(IRequestExecutor requestExecutor)
        {
            this.requestExecutor = requestExecutor ?? throw new ArgumentNullException(nameof(requestExecutor));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewPortfolio([FromBody] CreateNewPortfolioRequest request)
        {
            await requestExecutor.ExecuteCommand(new CreatePortfolioCommand(request.Name));
            return Ok();
        }
    }
}
