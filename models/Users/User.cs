using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using complainSystem.models.Complains;
using Microsoft.AspNetCore.Identity;

namespace complainSystem.models.Users
{
    public class User : IdentityUser
    {
  
     
      
        [Required]
        public string? Address { get; set; } = string.Empty;



        public string? Image { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public List<Complain>? Complaints { get; set; }

    }
}