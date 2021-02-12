using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Volta.BuildingBlocks.Application;
using Volta.UserAccess.Application.Commands.RegisterNewUser;
using Volta.UserAccess.Application.Models;

namespace Volta.UserAccess.WebApi.Controllers
{
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("api/[controller]")]
    public class UserAccessController : Controller
    {
        private readonly IRequestExecutor requestExecutor;

        public UserAccessController(IRequestExecutor requestExecutor)
        {
            this.requestExecutor = requestExecutor ?? throw new ArgumentNullException(nameof(requestExecutor));
        }

        [HttpPost]
        public async Task<Guid> CreateStock([FromBody] RegisterNewUserPostModel registerNewUserPostModel)
        {
            return await requestExecutor.ExecuteCommand(
                new RegisterNewUserCommand(registerNewUserPostModel.Email, registerNewUserPostModel.Password));
        }
    }
}
