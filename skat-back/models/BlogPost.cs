using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace skat_back.models;

public class BlogPost
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string UserId { get; set; } = null!;
    public required User User { get; set; }
    
    [Required]
    [MaxLength(255)]
    public required string Title { get; set; }
    
    [Required]
    [MaxLength(1000)]
    public required string Text { get; set; }
    
    [Required]
    [MaxLength(255)]
    public required string Slug { get; set; }
    
    [MaxLength(500)]
    public string? Summary { get; set; }

    [Required]
    [MaxLength(100)]
    public string Status { get; set; } = "Draft"; 
    
    [MaxLength(255)]
    public required string MetaTitle { get; set; }
    
    [MaxLength(500)]
    public required string MetaDescription { get; set; }

    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}