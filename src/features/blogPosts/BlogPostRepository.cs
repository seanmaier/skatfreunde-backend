using skat_back.data;
using skat_back.features.blogPosts.models;
using skat_back.Lib;

namespace skat_back.Features.BlogPosts;

public class BlogPostRepository(AppDbContext context)
    : Repository<BlogPost>(context), IBlogPostRepository
{
}