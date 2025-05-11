namespace skat_back.Features.BlogPosts;

/// <summary>
///     Represents a data transfer object (DTO) for creating a blog post.
/// </summary>
public record CreateBlogPostDto(
    Guid CreatedById,
    string Title,
    string Text,
    string Slug,
    string? Summary,
    BlogStatus Status,
    string MetaTitle,
    string MetaDescription
);