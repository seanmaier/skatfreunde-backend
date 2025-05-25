using Microsoft.AspNetCore.Mvc;
using skat_back.features.blogPosts;
using skat_back.features.blogPosts.models;
using skat_back.Lib;

namespace skat_back.Features.BlogPosts;

/// <summary>
///     Represents the API controller for managing blog posts.
/// </summary>
/// <param name="service">The injected Blogpost service</param>
[ApiController]
[Route("api/[controller]")]
public class BlogPostsController(IBlogPostService service)
    : BaseController<ResponseBlogPostDto, CreateBlogPostDto, UpdateBlogPostDto, int, IBlogPostService>(service)
{
}