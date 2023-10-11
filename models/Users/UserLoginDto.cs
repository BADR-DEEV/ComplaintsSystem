using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace complainSystem.models.Users
{
    public class UserLoginDto
    {
        [EmailAddress]
        public string? Email { get; set; }
        [DataType(DataType.Password)]

        public string? Password { get; set; } = string.Empty;

    }
}