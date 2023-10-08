using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using complainSystem.models;
using complainSystem.models.Users;

namespace complainSystem.Services.AuthenticationService
{
    public interface IAuthenticateUserService
    {
        Task<ServiceResponse<User>> RegisterUser(UserRegister authenticateUser , string role);
        Task<ServiceResponse<User>> LoginUser(UserLogin authenticateUser);
        Task<ServiceResponse<User>> Logout();
        
    }
}