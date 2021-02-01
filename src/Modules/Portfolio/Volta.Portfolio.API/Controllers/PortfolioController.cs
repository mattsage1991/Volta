using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volta.BuildingBlocks.Application;
using Volta.Portfolios.Application.Commands.CreatePortfolio;
using Volta.Portfolios.Application.Models;

namespace Volta.Portfolios.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class PortfolioController : ControllerBase
    {
        private readonly IRequestExecutor requestExecutor;

        public PortfolioController(IRequestExecutor requestExecutor)
        {
            this.requestExecutor = requestExecutor ?? throw new ArgumentNullException(nameof(requestExecutor));
        }

        [HttpPost]
        public async Task<Guid> CreateNewPortfolio([FromBody] PortfolioPostModel request)
        {
            return await requestExecutor.ExecuteCommand(new CreatePortfolioCommand(request.OwnerId, request.Name));
        }
    }
}
