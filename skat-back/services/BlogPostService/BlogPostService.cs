using skat_back.data;
using skat_back.models;

namespace skat_back.services.BlogPostService;

public class BlogPostService(AppDbContext db) : IBlogPostService
{
    public IEnumerable<BlogPost> GetAll()
    {
        return db.BlogPosts.ToList();
    }

    public BlogPost? GetById(string id)
    {
        return db.BlogPosts.Find(id);
    }

    public void Add(BlogPost blogPost)
    {
        db.Add(blogPost);   
        db.SaveChanges();
    }

    public void Update(string id, BlogPost updatedBlogPost)
    {
        var existingBlogPost = db.BlogPosts.Find(id);
        if (existingBlogPost == null)
            throw new Exception("BlogPost not found");

        existingBlogPost.Title = updatedBlogPost.Title;
        existingBlogPost.Text = updatedBlogPost.Text;
        existingBlogPost.Slug = updatedBlogPost.Slug;
        existingBlogPost.Summary = updatedBlogPost.Summary;
        existingBlogPost.Status = updatedBlogPost.Status;
        existingBlogPost.MetaTitle = updatedBlogPost.MetaTitle;
        existingBlogPost.MetaDescription = updatedBlogPost.MetaDescription;
        existingBlogPost.UpdatedAt = DateTime.UtcNow;

        db.SaveChanges();
    }

    public void Delete(string id)
    {
        var blogPost = db.BlogPosts.Find(id);
        if (blogPost == null)
            throw new Exception("BlogPost not found");
        db.BlogPosts.Remove(blogPost);
        db.SaveChanges();
    }
}