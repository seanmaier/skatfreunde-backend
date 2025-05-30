using System.ComponentModel.DataAnnotations;
using skat_back.features.auth.models;
using skat_back.Lib;
using static skat_back.utilities.constants.ValidationConstants;

namespace skat_back.features.blogPosts.models;

/// <summary>
///     The BlogPost class represents a blog post entity for the Database
/// </summary>
public class BlogPost : BaseEntity
{
    [MaxLength(MaxTextLength)] public required string Title { get; set; }
    [MaxLength(MaxTextLength)] public required string Text { get; set; }
    [MaxLength(MaxSlugLength)] public required string Slug { get; set; }
    [MaxLength(MaxSummaryLength)] public string? Summary { get; set; }
    public BlogStatus Status { get; set; } = BlogStatus.Draft;
    [MaxLength(MaxTitleLength)] public required string MetaTitle { get; set; }
    [MaxLength(MaxDescriptionLength)] public required string MetaDescription { get; set; }
    
    /*------------------------Updater Logic------------------------*/
    public void UpdateFrom(BlogPost blogPost)
    {
        Title = blogPost.Title;
        Text = blogPost.Text;
        Slug = blogPost.Slug;
        Summary = blogPost.Summary;
        Status = blogPost.Status;
        MetaTitle = blogPost.MetaTitle;
        MetaDescription = blogPost.MetaDescription;
        UpdatedById = blogPost.UpdatedById;
    }
}