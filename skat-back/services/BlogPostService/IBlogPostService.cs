using skat_back.dto.BlogPostDto;
using skat_back.models;

namespace skat_back.services.BlogPostService;

public interface IBlogPostService : IBaseService<BlogPost, BlogPostRequest, BlogPostRequest, int>
{
}