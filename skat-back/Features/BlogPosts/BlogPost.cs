using System.ComponentModel.DataAnnotations;
using skat_back.models;
using static skat_back.utilities.constants.ValidationConstants;

namespace skat_back.Features.BlogPosts;


public class BlogPost : BaseEntity
{
    [Key] public int Id { get; set; }

    [MaxLength(MaxTextLength)] public required string Title { get; set; }
    [MaxLength(MaxTextLength)] public required string Text { get; set; }
    [MaxLength(MaxSlugLength)] public required string Slug { get; set; }
    [MaxLength(MaxSummaryLength)] public string? Summary { get; set; }
    public BlogStatus Status { get; set; } = BlogStatus.Draft;
    [MaxLength(MaxTitleLength)] public required string MetaTitle { get; set; }
    [MaxLength(MaxDescriptionLength)] public required string MetaDescription { get; set; }

    /*--------------------Navigation  Properties--------------------*/

    [Required] public required Guid CreatedById { get; set; }
    public User CreatedBy { get; set; } = null!;
}