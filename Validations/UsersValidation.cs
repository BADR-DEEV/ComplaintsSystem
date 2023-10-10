using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using complainSystem.models.Users;
using FluentValidation;

namespace complainSystem.Validations
{
    public class UsersValidation : AbstractValidator<UserLogin>
    {
        public UsersValidation()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Password is required");
        }
        
    }

    public class UserRegisterValidation : AbstractValidator<UserRegister>
    {
        public UserRegisterValidation()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Password is required");
            RuleFor(x => x.PhoneNumber).NotEmpty().MinimumLength(10).WithMessage("Phone Number is required");
        }
        
    }
}