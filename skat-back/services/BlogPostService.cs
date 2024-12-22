using Microsoft.EntityFrameworkCore;
using skat_back.data;

namespace skat_back.services;

public class BlogPostService
{
    private readonly AppDbContext _context;

    public BlogPostService(AppDbContext context)
    {
        _context = context;
    }

    public BlogPost? GetBlogPost(int id)
    {
        return _context.BlogPosts.Include(b => b.User).FirstOrDefault(b => b.UserId == id);
    }

    public List<BlogPost> GetAllBlogPosts()
    {
        return _context.BlogPosts.ToList();
    }

    public void AddBlogPost(BlogPost blogPost)
    {
        _context.BlogPosts.Add(blogPost);
        _context.SaveChanges();
    }

    public void UpdateBlogPost(BlogPost updated, int id)
    {
        var blogPost = _context.BlogPosts.Find(id);
        
        if (blogPost == null) return;
        
        blogPost.Status = updated.Status;
        blogPost.Summary = updated.Summary;
        blogPost.Text = updated.Text;
        blogPost.User = updated.User;
        blogPost.MetaDescription = updated.MetaDescription;
        blogPost.MetaTitle = updated.MetaTitle;
        blogPost.UpdatedAt = DateTime.UtcNow;
    }

    public void DeleteBlogPost(int id)
    {
        var blogPost = _context.BlogPosts.Find(id);
        
        if (blogPost == null) return;
        
        _context.BlogPosts.Remove(blogPost);
        _context.SaveChanges();
    }
}