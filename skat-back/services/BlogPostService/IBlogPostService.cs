using skat_back.dto.BlogPostDto;
using skat_back.models;

namespace skat_back.services.BlogPostService;

public interface IBlogPostService
{
    Task<IEnumerable<BlogPost>> GetAllAsync();
    Task<BlogPost?> GetByIdAsync(int id);
    Task<BlogPost> CreateAsync(BlogPostRequest dto);
    Task<bool> UpdateAsync(int id, BlogPostRequest dto);
    Task<bool> DeleteAsync(int id);
}