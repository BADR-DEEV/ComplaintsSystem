using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using complainSystem.models.Users;
using FluentValidation;

namespace complainSystem.Validations
{

    public class UserRegisterValidation : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterValidation()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Password is required");
            RuleFor(x => x.PhoneNumber).NotEmpty().MinimumLength(8).WithMessage("Phone Number is required");
        }

    }

    public class UserLoginValidation : AbstractValidator<UserLoginDto>
    {
        public UserLoginValidation()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email is required").NotNull();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Password is required").Length(6, 100).NotNull();
        }

    }
}