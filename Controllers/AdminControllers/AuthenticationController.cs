using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
            ServiceResponse<User> response = await _AuthenticateUserService.RegisterUser(authenticateUser, role);
           
            switch (response.StatusCode)
            {
                case 500:
                    return StatusCode(500, response);

                case 400:
                    return BadRequest(response);
                case 201:
                    return Created("", response);

                default:
                    return BadRequest(response);
            }


        }

        [HttpPost]
        [Route("LoginUser")]
        public async Task<ActionResult<ServiceResponse<User>>> LoginUser([FromBody] UserLogin authenticateUser)
        {
            ServiceResponse<User> response = await _AuthenticateUserService.LoginUser(authenticateUser);
           
            switch (response.StatusCode)
            {
                case 500:
                    return StatusCode(500, response);

                case 400:
                    return BadRequest(response);
                case 200:
                    return Ok(response);

                default:
                    return BadRequest(response);
            }

        }
    }
}