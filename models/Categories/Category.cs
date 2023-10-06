
namespace ComplainSystem.models
{
    public class Category
    {
        
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        //for admin 
        // public bool IsDeleted { get; set; } = false;
        // public DateTime CreatedAt { get; set; } = DateTime.Now;
        // public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}