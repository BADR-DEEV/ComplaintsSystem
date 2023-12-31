using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using complainSystem.models;

namespace complainSystem.models.Complains
{
    public class UpdateComplainDto
    {
        public int Id { get; set; }

        public string ComplainTitle { get; set; } = string.Empty;
        public string ComplainDescription { get; set; } = string.Empty;
        public ComplainStatus ComplainStatus { get; set; } = ComplainStatus.Open;
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

    }
}