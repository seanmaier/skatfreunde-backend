using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace skat_back.data;

public class BlogPost
{
    [Key] 
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string Title { get; set; }
    
    [Required]
    public string Text { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string Slug { get; set; }
    
    [MaxLength(500)]
    public string Summary { get; set; }

    [Required] public string Status { get; set; } = "Draft"; 
    
    [MaxLength(255)]
    public string MetaTitle { get; set; }
    
    [MaxLength(500)]
    public string MetaDescription { get; set; }

    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}