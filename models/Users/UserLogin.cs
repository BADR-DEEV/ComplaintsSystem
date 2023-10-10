using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace complainSystem.models.Users
{
    public class UserLogin
    {
        [EmailAddress]
        public string? Email { get; set; }
        public string? Password { get; set; } = string.Empty;

    }
}