using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ComplainSystem.models;
using complainSystem.models;

namespace complainSystem.models.ComplainDto
{
    public class AddComplainDto
    {

        public string? ComplainTitle { get; set; } = string.Empty;
        public string ComplainDescription { get; set; } = string.Empty;
        public DateTime ComplainDateTime { get; set; } = DateTime.Now;
        public ComplainStatus ComplainStatus { get; set; } = ComplainStatus.Open;
        
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

    }
}