namespace skat_back.features.blogPosts;

/// <summary>
///     Represents a data transfer object (DTO) for a blog post.
/// </summary>
public record ResponseBlogPostDto(
    int Id,
    Guid CreatedById,
    string Title,
    string Text,
    string Slug,
    string? Summary,
    BlogStatus Status,
    string MetaTitle,
    string MetaDescription,
    DateTime CreatedAt,
    DateTime UpdatedAt
);