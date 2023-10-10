using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using complainSystem.models;
using complainSystem.models.Users;
using ComplainSystem.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace complainSystem.Services.AuthenticationService
{
    public class AuthenticateUserService : IAuthenticateUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<SymmetricSecurityKey> _logger;

        ServiceResponse<User> serviceResponse = new ServiceResponse<User>();

        public AuthenticateUserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
         SignInManager<User> signInManager, IMapper mapper, IConfiguration configuration)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _configuration = configuration;
        }


        public async Task<ServiceResponse<User>> RegisterUser(UserRegister registerDto, string role)
        {
            try
            {
                User? userExist = await _userManager.FindByEmailAsync(registerDto.Email);
                if (userExist != null)
                {

                    serviceResponse.Message = "User already exist";
                    serviceResponse.Success = false;
                    serviceResponse.StatusCode = 400;
                    return serviceResponse;

                }


                if (await _roleManager.RoleExistsAsync(role))
                {
                    var user = _mapper.Map<User>(registerDto);
                    user.CreatedAt = DateTime.Now;
                    user.SecurityStamp = Guid.NewGuid().ToString();
                    user.UserName = registerDto.Email;


                    var result = await _userManager.CreateAsync(user, registerDto.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, role!);
                        serviceResponse.Data = user;
                        serviceResponse.Message = "User created successfully";
                        serviceResponse.Success = true;
                        serviceResponse.StatusCode = 201;
                        return serviceResponse;

                    }
                    else
                    {
                        serviceResponse.Message = "User creation failed";
                        serviceResponse.Success = false;
                        serviceResponse.StatusCode = 400;
                        return serviceResponse;

                    }

                }
                else
                {
                    serviceResponse.Message = "Role does not exist";
                    serviceResponse.Success = false;
                    serviceResponse.StatusCode = 400;
                    return serviceResponse;

                }
            }
            catch (Exception e)
            {
                serviceResponse.Message = e.Message;
                serviceResponse.Success = false;
                serviceResponse.StatusCode = 500;
                return serviceResponse;

            }


        }



        public async Task<ServiceResponse<User>> LoginUser(UserLogin userlogin)
        {
            try
            {
                var userExist = await _userManager.FindByEmailAsync(userlogin.Email);
                if (userExist == null)
                {
                    serviceResponse.Message = "User does not exist";
                    serviceResponse.Success = false;
                    serviceResponse.StatusCode = 400;
                    return serviceResponse;
                }
                else
                {
                    // var result = _userManager.CheckPasswordAsync(userExist.Result, userlogin.Password);
                    var result = await _signInManager.PasswordSignInAsync(userlogin.Email, userlogin.Password, false, false);

                    if (result.Succeeded && userlogin.Email == userExist.Email)
                    {
                        var token = RefreshToken(userlogin);


                        serviceResponse.Data = userExist;
                        serviceResponse.Message = "User login successfully";
                        serviceResponse.Token = token;
                        serviceResponse.Success = true;
                        serviceResponse.StatusCode = 200;
                        return serviceResponse;
                        // serviceResponse.Message = token;
                        // serviceResponse.Success = true;
                        // serviceResponse.StatusCode = 200;
                        // return serviceResponse;
                    }
                    else
                    {
                        serviceResponse.Message = "User login failed";
                        serviceResponse.Success = false;
                        serviceResponse.StatusCode = 400;
                        return serviceResponse;
                    }
                }
            }
            catch (Exception e)
            {
                serviceResponse.Message = e.Message;
                serviceResponse.Success = false;
                serviceResponse.StatusCode = 500;
                return serviceResponse;

            }
        }
        public Task<ServiceResponse<User>> Logout()
        {
            throw new NotImplementedException();
        }

        public string RefreshToken(UserLogin user)
        {
            List<Claim> claims = new List<Claim>{
                new(ClaimTypes.Email, user.Email ),
                new(ClaimTypes.Role, "User" ),
            };
            var Security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            SigningCredentials credentials = new SigningCredentials(Security, SecurityAlgorithms.HmacSha256Signature);

            SecurityToken securityToken = new JwtSecurityToken(

                claims: claims,
                issuer: _configuration.GetSection("AppSettings:Issuer").Value,
                audience: _configuration.GetSection("AppSettings:Audience").Value,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );


            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;


        }
    }
}
