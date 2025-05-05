namespace skat_back.dto.BlogPostDto;

public record BlogPostRequest(
    int Id,
    Guid UserId,
    string Title,
    string Text,
    string Slug,
    string? Summary,
    BlogStatus Status,
    string MetaTitle,
    string MetaDescription
);