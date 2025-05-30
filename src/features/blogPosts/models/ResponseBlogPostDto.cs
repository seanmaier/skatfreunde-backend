namespace skat_back.features.blogPosts.models;

/// <summary>
///     Represents a data transfer object (DTO) for a blog post.
/// </summary>
public record ResponseBlogPostDto(
    int Id,
    string Title,
    string Text,
    string Slug,
    string? Summary,
    BlogStatus Status,
    string MetaTitle,
    string MetaDescription,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string CreatedById,
    string? UpdatedById = null
);