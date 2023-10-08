using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using complainSystem.models;
using complainSystem.models.Users;
using ComplainSystem.Data;
using Microsoft.AspNetCore.Identity;

namespace complainSystem.Services.AuthenticationService
{
    public class AuthenticateUserService : IAuthenticateUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public AuthenticateUserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<User>> RegisterUser(UserRegister registerDto, string role)
        {
            try
            {
                User? userExist = await _userManager.FindByEmailAsync(registerDto.Email);
                if (userExist != null)
                {
                    return new ServiceResponse<User>
                    {
                        Data = null,
                        Message = "User already exist",
                        Success = false,
                        StatusCode = 400
                    };
                }


                if (await _roleManager.RoleExistsAsync(role))
                {
                    var user = _mapper.Map<User>(registerDto);
                    user.CreatedAt = DateTime.Now;
                    user.SecurityStamp = Guid.NewGuid().ToString();
                    user.UserName = registerDto.Email;
    

                    // User user = new User
                    // {
                    //     UserName = registerDto.UserName,
                    //     Email = registerDto.Email,
                    //     PhoneNumber = registerDto.PhoneNumber,
                    //     SecurityStamp = Guid.NewGuid().ToString(),
                    //     // Address = null,
                    //     // City = null,
                    //     // Country = null,
                    //     // Image = null,
                    //     CreatedAt = DateTime.Now,
                    //     // UpdatedAt = null,
                    //     Complaints = null,
                    //     Role = role != null ? await _roleManager.FindByNameAsync(role) : null

                    // };
                    var result = await _userManager.CreateAsync(user, registerDto.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, role!);
                        return new ServiceResponse<User>
                        {
                            Data = user,
                            Message = "User created successfully",
                            Success = true,
                            StatusCode = 201
                        };
                    }
                    else
                    {
                        return new ServiceResponse<User>
                        {
                            Data = null,
                            Message = "User creation failed",
                            Success = false,
                            StatusCode = 400
                        };
                    }

                }
                else
                {
                    return new ServiceResponse<User>
                    {

                        Data = null,
                        Message = "Role does not exist",
                        Success = false,
                        StatusCode = 400
                    };
                }
            }
            catch (Exception e)
            {
                new ServiceResponse<User>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false,
                    StatusCode = 500
                };
                return new ServiceResponse<User>()
                {
                    Data = null,
                    Message = e.Message,
                    Success = false,
                    StatusCode = 500
                };
            }


        }



        public async Task<ServiceResponse<User>> LoginUser(UserLogin userlogin)
        {
            try {
                var userExist = await _userManager.FindByEmailAsync(userlogin.Email);
                if (userExist == null)
                {
                    return new ServiceResponse<User>
                    {
                        Data = null,
                        Message = "User does not exist",
                        Success = false,
                        StatusCode = 400
                    };
                }
                else
                {
                    // var result = _userManager.CheckPasswordAsync(userExist.Result, userlogin.Password);
                    var result = await _signInManager.PasswordSignInAsync(userlogin.Email, userlogin.Password, false, false);

                    if (result.Succeeded && userlogin.Email == userExist.Email)
                    {
                        return new ServiceResponse<User>
                        {
                            Data = userExist,
                            Message = "User logged in successfully",
                            Success = true,
                            StatusCode = 200
                        };
                    }
                    else
                    {
                        return new ServiceResponse<User>
                        {
                            Data = null,
                            Message = "User login failed",
                            Success = false,
                            StatusCode = 400
                        };
                    }
                }
            }
            catch (Exception e)
            {
               return new ServiceResponse<User>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false,
                    StatusCode = 500
                };
               
            }
        }
        public Task<ServiceResponse<User>> Logout()
        {
            throw new NotImplementedException();
        }
    }
}
