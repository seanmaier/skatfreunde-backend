namespace skat_back.features.blogPosts.models;

/// <summary>
///     Represents a data transfer object (DTO) for creating a blog post.
/// </summary>
public record CreateBlogPostDto(
    string CreatedById,
    string Title,
    string Text,
    string Slug,
    string? Summary,
    BlogStatus Status,
    string MetaTitle,
    string MetaDescription
);