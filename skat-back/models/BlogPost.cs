using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static skat_back.constants.ValidationConstats;

namespace skat_back.models;

public class BlogPost
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;

    [Required]
    [MaxLength(MaxIdLength)]
    public string UserId { get; set; } = null!;
    public required User User { get; set; }
    
    [Required]
    [MaxLength(BlogPostTitleMaxLength)]
    public required string Title { get; set; }
    
    [Required]
    [MaxLength(BlogPostContentMaxLength)]
    public required string Text { get; set; }
    
    [Required]
    [MaxLength(BlogPostSlugMaxLength)]
    public required string Slug { get; set; }
    
    [MaxLength(BlogPostSummaryMaxLength)]
    public string? Summary { get; set; }

    [Required]
    [MaxLength(BlogPostStatusMaxLength)]
    public string Status { get; set; } = "Draft"; 
    
    [MaxLength(BlogPostMetaTitleMaxLength)]
    public required string MetaTitle { get; set; }
    
    [MaxLength(BlogPostMetaDescriptionMaxLength)]
    public required string MetaDescription { get; set; }

    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}