using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.features.blogPosts;
using skat_back.utilities.mapping;
using ILogger = Serilog.ILogger;

namespace skat_back.Features.BlogPosts;

/// <summary>
///     Represents the service for managing blog posts.
/// </summary>
/// <param name="db">The Database context</param>
/// <param name="uow">To be removed</param>
/// <param name="logger">The injected Logger</param>
public class BlogPostService(AppDbContext db, IUnitOfWork uow, ILogger logger) : IBlogPostService
{
    public async Task<ICollection<ResponseBlogPostDto>> GetAllAsync()
    {
        logger.Information("Fetching all blog posts");
        return await db.BlogPosts.Select(bp => bp.ToDto()).ToListAsync();
    }

    public async Task<ResponseBlogPostDto?> GetByIdAsync(int id)
    {
        logger.Information("Fetching blog post with ID: {Id}", id);
        var blogPost = await db.BlogPosts.FindAsync(id);
        return blogPost?.ToDto();
    }

    public async Task<ResponseBlogPostDto> CreateAsync(CreateBlogPostDto dto)
    {
        logger.Information("Creating blog post: {@BlogPost}", dto);

        try
        {
            var blogPost = dto.ToEntity();

            db.BlogPosts.Add(blogPost);
            await uow.CommitAsync();

            return blogPost.ToDto();
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error creating blog post: {@BlogPost}", dto);
            throw;
        }
    }

    public async Task<bool> UpdateAsync(int id, UpdateBlogPostDto dto)
    {
        logger.Information("Updating blog post with ID: {Id}", id);

        try
        {
            var existingBlogPost = await db.BlogPosts.FindAsync(id);
            if (existingBlogPost == null)
                return false;

            existingBlogPost.Title = dto.Title;
            existingBlogPost.Text = dto.Text;
            existingBlogPost.Slug = dto.Slug;
            existingBlogPost.Summary = dto.Summary;
            existingBlogPost.Status = dto.Status;
            existingBlogPost.MetaTitle = dto.MetaTitle;
            existingBlogPost.MetaDescription = dto.MetaDescription;
            existingBlogPost.UpdatedAt = DateTime.UtcNow;

            await uow.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error updating blog post with ID: {Id}", id);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        logger.Information("Deleting blog post with ID: {Id}", id);
        try
        {
            var blogPost = await db.BlogPosts.FindAsync(id);
            if (blogPost == null)
                return false;

            db.BlogPosts.Remove(blogPost);
            await uow.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error deleting blog post with ID: {Id}", id);
            throw;
        }
    }
}