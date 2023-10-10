using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ComplainSystem.models;
using complainSystem.models;
using complainSystem.models.Users;

namespace complainSystem.models.Complains
{
    public class Complain
    {

        public int Id { get; set; }

        public string ComplainTitle { get; set; } = string.Empty;
        public string ComplainDescription { get; set; } = string.Empty;
        public DateTime ComplainDateTime { get; set; } = DateTime.Now;
        public ComplainStatus ComplainStatus { get; set; } = ComplainStatus.Open;
        public Category? Category { get; set; }
        public User? User { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        // public string ComplainPriority { get; set; }
        // public string? ComplainImage { get; set; }
        // public string? ComplainLocation { get; set; }
        // public string ComplainAddress { get; set; }
        // public string ComplainCity { get; set; }
        // public string? ComplainLatitude { get; set; }
        // public string? ComplainLongitude { get; set; }

    }
}