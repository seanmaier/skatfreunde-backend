using Microsoft.AspNetCore.Mvc;
using skat_back.dto.BlogPostDto;
using skat_back.models;
using skat_back.services.BlogPostService;

namespace skat_back.controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogPostsController(IBlogPostService service)
    : GenericController<BlogPost, BlogPostRequest, BlogPostRequest, int, IBlogPostService>(service)
{
}