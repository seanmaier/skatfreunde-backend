using skat_back.features.blogPosts.models;
using skat_back.Lib;
using skat_back.utilities.mapping;

namespace skat_back.Features.BlogPosts;

/// <summary>
///     Represents the service for managing blog posts.
/// </summary>
public class BlogPostService(IUnitOfWork unitOfWork) : IBlogPostService
{
    public async Task<PagedResult<ResponseBlogPostDto>> GetAllAsync(PaginationParameters parameters)
    {
        var blogPosts = await unitOfWork.BlogPosts.GetAllAsync(parameters);
        return blogPosts.ToPagedResult();
    }

    public async Task<ResponseBlogPostDto?> GetByIdAsync(int id)
    {
        var blogPost = await unitOfWork.BlogPosts.GetByIdAsync(id);
        return blogPost?.ToResponse();
    }

    public async Task<ResponseBlogPostDto> CreateAsync(CreateBlogPostDto dto)
    {
        var blogPost = dto.ToEntity();

        await unitOfWork.BlogPosts.CreateAsync(blogPost);

        await unitOfWork.SaveChangesAsync();

        return blogPost.ToResponse();
    }

    public async Task<bool> UpdateAsync(int id, UpdateBlogPostDto dto)
    {
        var existing = await unitOfWork.BlogPosts.GetByIdAsync(id);
        if (existing == null)
            return false;

        var updatedBlogPost = dto.ToEntity();
        existing.UpdateFrom(updatedBlogPost);

        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var blogPost = await unitOfWork.BlogPosts.GetByIdAsync(id);
        if (blogPost == null)
            return false;

        unitOfWork.BlogPosts.Delete(blogPost);
        await unitOfWork.SaveChangesAsync();

        return true;
    }
}