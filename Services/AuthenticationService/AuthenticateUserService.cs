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
using complainSystem.Validations;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        ServiceResponse<User> serviceResponse = new ServiceResponse<User>();

        public AuthenticateUserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
         SignInManager<User> signInManager, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<ServiceResponse<User>> RegisterUser(UserRegisterDto registerDto, string role)
        {

            var validationResult = new UserRegisterValidation().Validate(registerDto);
            var mapped = _mapper.Map<User>(registerDto);
            ReqguestValidationGeneric<User> req = new(validationResult.IsValid, mapped, validationResult.Errors);
            if (!validationResult.IsValid)
            {
                req.serviceResponse!.StatusCode = 400;
                req.serviceResponse.Success = false;
                return req.serviceResponse;
            }

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



        public async Task<ServiceResponse<User>> LoginUser(UserLoginDto userlogin)
        {
            var validationResult = new UserLoginValidation().Validate(userlogin);
            var mapped = _mapper.Map<User>(userlogin);
            ReqguestValidationGeneric<User> req = new(validationResult.IsValid, mapped, validationResult.Errors);
            if (!validationResult.IsValid)
            {
                req.serviceResponse!.StatusCode = 400;
                req.serviceResponse.Success = false;
                serviceResponse.Message = "Validation Error";
                return req.serviceResponse;
            }
            try
            {
                User? userExist = await _userManager.FindByEmailAsync(userlogin.Email);
                if (userExist == null)
                {
                    serviceResponse.Message = "User does not exist";
                    serviceResponse.Success = false;
                    serviceResponse.StatusCode = 404;
                    return serviceResponse;
                }
                else
                {
                    var result = await _signInManager.PasswordSignInAsync(userlogin.Email, userlogin.Password, false, false);

                    if (result.Succeeded && userlogin.Email == userExist.Email)
                    {
                        var token = RefreshToken(mapped);
                        serviceResponse.Data = userExist;
                        serviceResponse.Message = "User login successfully";
                        serviceResponse.access_token = token;
                        serviceResponse.Success = true;
                        serviceResponse.StatusCode = 200;
                        return serviceResponse;
                    }
                    else
                    {
                        serviceResponse.Data = userExist;
                        serviceResponse.Message = "User login failed  password in incorrect";
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

        public string RefreshToken(User user)
        {

            var Security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value ?? "some default key"));
            SigningCredentials credentials = new SigningCredentials(Security, SecurityAlgorithms.HmacSha256Signature);
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id!),
                new Claim(ClaimTypes.Role , "User"),
                new Claim(ClaimTypes.Email, user.Email!),

            };

            // SecurityToken securityToken = new JwtSecurityToken(
            //     claims: claims,

            //     issuer: _configuration.GetSection("AppSettings:Issuer").Value,
            //     audience: _configuration.GetSection("AppSettings:Audience").Value,
            //     expires: DateTime.Now.AddMinutes(30),
            //     signingCredentials: credentials
            // );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials,
                Issuer = _configuration.GetSection("AppSettings:Issuer").Value,
                Audience = _configuration.GetSection("AppSettings:Audience").Value,
                


            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;


        }
    }
}
