
using System.ComponentModel.DataAnnotations;

namespace ComplainSystem.models
{
    public class Category
    {

        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Name { get; set; } = string.Empty;
        [StringLength(500)]
        public string? Description { get; set; } = string.Empty;

        //for admin 
        // public bool IsDeleted { get; set; } = false;
        // public DateTime CreatedAt { get; set; } = DateTime.Now;
        // public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}