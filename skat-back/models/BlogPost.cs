using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static skat_back.utilities.constants.ValidationConstants;

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
    [MaxLength(MaxTextLength)]
    public required string Title { get; set; }
    
    [Required]
    [MaxLength(MaxTextLength)]
    public required string Text { get; set; }
    
    [Required]
    [MaxLength(MaxSlugLength)]
    public required string Slug { get; set; }
    
    [MaxLength(MaxSummaryLength)]
    public string? Summary { get; set; }

    [Required]
    public BlogStatus Status { get; set; } = BlogStatus.Draft; 
    
    [MaxLength(MaxTitleLength)]
    public required string MetaTitle { get; set; }
    
    [MaxLength(MaxDescriptionLength)]
    public required string MetaDescription { get; set; }

    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}