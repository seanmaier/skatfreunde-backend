namespace skat_back.features.blogPosts.models;

/// <summary>
///     Represents a data transfer object (DTO) for updating a blog post.
/// </summary>
public record UpdateBlogPostDto(
    int Id,
    string CreatedById,
    string Title,
    string Text,
    string Slug,
    string? Summary,
    BlogStatus Status,
    string MetaTitle,
    string MetaDescription
);