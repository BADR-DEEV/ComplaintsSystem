using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ComplainSystem.models;
using complainSystem.models;

namespace complainSystem.models.Complains
{
     public class Complain
    {
    
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string ComplainTitle { get; set; } = string.Empty;
        [StringLength(500)]
        public string ComplainDescription { get; set; } = string.Empty;
        public DateTime ComplainDateTime { get; set; } = DateTime.Now;
        public ComplainStatus ComplainStatus { get; set; } = ComplainStatus.Open;
        public Category? Category { get; set; }
        [ForeignKey("CategoryId")]
        [Required]

        public int  CategoryId { get; set; }
        
        // public string ComplainPriority { get; set; }
        // public string? ComplainImage { get; set; }
        // public string? ComplainLocation { get; set; }
        // public string ComplainAddress { get; set; }
        // public string ComplainCity { get; set; }
        // public string? ComplainLatitude { get; set; }
        // public string? ComplainLongitude { get; set; }

    }
}