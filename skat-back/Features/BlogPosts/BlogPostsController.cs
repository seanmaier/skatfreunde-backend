using Microsoft.AspNetCore.Mvc;
using skat_back.dto.BlogPostDto;
using skat_back.services.BlogPostService;

namespace skat_back.controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogPostsController(IBlogPostService service)
    : BaseController<ResponseBlogPostDto, CreateBlogPostDto, UpdateBlogPostDto, int, IBlogPostService>(service)
{
}