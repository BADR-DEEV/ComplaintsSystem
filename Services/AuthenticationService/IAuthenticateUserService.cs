using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal.Mappers;
using complainSystem.models;
using complainSystem.models.Users;

namespace complainSystem.Services.AuthenticationService
{
    public interface IAuthenticateUserService
    {
        Task<ServiceResponse<User>> RegisterUser(UserRegisterDto authenticateUser , string role);
        Task<ServiceResponse<User>> LoginUser(UserLoginDto authenticateUser);
        Task<ServiceResponse<User>> Logout();
        string RefreshToken(User authenticateUser);
        
    }
}