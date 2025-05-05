using Microsoft.AspNetCore.Mvc;
using skat_back.dto.BlogPostDto;
using skat_back.models;
using skat_back.services.BlogPostService;

namespace skat_back.controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogPostsController(IBlogPostService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllBlogPosts()
    {
        IEnumerable<BlogPost> blogPosts = await service.GetAllAsync();
        return Ok(blogPosts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBlogPostById(int id)
    {
        var blogPost = await service.GetByIdAsync(id);
        if (blogPost == null)
            return NotFound();

        return Ok(blogPost);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBlogPost([FromBody] BlogPostRequest dto)
    {
        var blogPost = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetBlogPostById), new { id = blogPost.Id }, blogPost);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBlogPost(int id, [FromBody] BlogPostRequest dto)
    {
        var updated = await service.UpdateAsync(id, dto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlogPost(int id)
    {
        var deleted = await service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}