using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using complainSystem.Helpers;
using complainSystem.models;
using complainSystem.models.Users;
using complainSystem.Services.AuthenticationService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace complainSystem.Controllers.AdminControllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticateUserService _AuthenticateUserService;
        public AuthenticationController(IAuthenticateUserService authenticateUserService)
        {
            _AuthenticateUserService = authenticateUserService;

        }

        [HttpPost]
        [Route("RegisterUser")]
        public async Task<ActionResult<ServiceResponse<User>>> RegisterUser([FromBody] UserRegister authenticateUser, string role)
        {
            Helpers<User> helper = new();
            return helper.HandleResponse(await _AuthenticateUserService.RegisterUser(authenticateUser, role));

        }

        [HttpPost]
        [Route("LoginUser")]
        public async Task<ActionResult<ServiceResponse<User>>> LoginUser([FromBody] UserLogin authenticateUser)
        {
            Helpers<User> helper = new();
            return helper.HandleResponse(await _AuthenticateUserService.LoginUser(authenticateUser));

        }
    }
}