using AutoMapper;
using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.dto.BlogPostDto;
using skat_back.models;
using ILogger = Serilog.ILogger;

namespace skat_back.services.BlogPostService;

public class BlogPostService(AppDbContext db, IUnitOfWork uow, IMapper mapper, ILogger logger) : IBlogPostService
{
    public async Task<IEnumerable<BlogPost>> GetAllAsync()
    {
        logger.Information("Fetching all blog posts");
        return await db.BlogPosts.ToListAsync();
    }

    public async Task<BlogPost?> GetByIdAsync(int id)
    {
        logger.Information("Fetching blog post with ID: {Id}", id);
        return await db.BlogPosts.FindAsync(id);
    }

    public async Task<BlogPost> CreateAsync(BlogPostRequest dto)
    {
        logger.Information("Creating blog post: {@BlogPost}", dto);

        try
        {
            var blogPost = mapper.Map<BlogPost>(dto);
            
            db.Add(blogPost);
            await uow.CommitAsync();

            return blogPost;
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error creating blog post: {@BlogPost}", dto);
            throw;
        }
    }

    public async Task<bool> UpdateAsync(int id, BlogPostRequest dto)
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