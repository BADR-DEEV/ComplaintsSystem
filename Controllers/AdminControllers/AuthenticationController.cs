using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using complainSystem.Helpers;
using complainSystem.models;
using complainSystem.models.Users;
using complainSystem.Services.AuthenticationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace complainSystem.Controllers.AdminControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticateUserService _AuthenticateUserService;
        private readonly UserManager<User> _userManager;
        public AuthenticationController(IAuthenticateUserService authenticateUserService, UserManager<User> userManager)
        {
            _AuthenticateUserService = authenticateUserService;
            _userManager = userManager;

        }

        [HttpPost]
        [Route("RegisterUser")]
        public async Task<ActionResult<ServiceResponse<User>>> RegisterUser([FromBody] UserRegisterDto authenticateUser, string role)
        {
            Helpers<User> helper = new();
            return helper.HandleResponse(await _AuthenticateUserService.RegisterUser(authenticateUser, role));

        }

        [HttpPost]
        [Route("LoginUser")]

        public async Task<ActionResult<ServiceResponse<User>>> LoginUser([FromBody] UserLoginDto authenticateUser)
        {
    
            Helpers<User> helper = new();
            return helper.HandleResponse(await _AuthenticateUserService.LoginUser(authenticateUser));

        }
    }
}