using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using complainSystem.models.Complains;
using Microsoft.AspNetCore.Identity;

namespace complainSystem.models.Users
{
    public class UserRegisterDto
    {

        [EmailAddress]
        public string? Email { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        public string? Password { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        // public DateTime? UpdatedAt { get; set; }
        // public List<Complain>? Complaints { get; set; }

    }
}