using skat_back.data;
using skat_back.services.BlogPostService;

namespace skat_back.controllers;

public class BlogPostController: BaseController<BlogPost, BlogPostService>
{
    public BlogPostController(BlogPostService service): base(service){}
}