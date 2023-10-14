using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace complainSystem.models.Users
{
    
    public class PersonUserDto
    {
        public string? Id { get; set; }
        public string? Email { get; set; } 
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}