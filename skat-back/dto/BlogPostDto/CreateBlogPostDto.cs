namespace skat_back.dto.BlogPostDto;

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