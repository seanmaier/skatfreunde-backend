using skat_back.dto.BlogPostDto;

namespace skat_back.services.BlogPostService;

public interface IBlogPostService : IService<ResponseBlogPostDto, CreateBlogPostDto, UpdateBlogPostDto, int>
{
}