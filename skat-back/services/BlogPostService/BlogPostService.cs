using skat_back.models;
using skat_back.repositories;

namespace skat_back.services.BlogPostService;

public class BlogPostService(IRepository<BlogPost> repository) : IBlogPostService
{
    public BlogPost? GetById(int id)
    {
        return repository.GetById(id);
    }

    public IEnumerable<BlogPost> GetAll()
    {
        return repository.GetAll();
    }

    public void Add(BlogPost blogPost)
    {
        repository.Add(blogPost);
    }

    public void Update(int id, BlogPost updatedBlogPost)
    {
        repository.Update(id, updatedBlogPost, (existing, updated) =>
        {
            existing.User = updated.User;
            existing.UserId = updated.UserId;
            existing.UpdatedAt = existing.UpdatedAt;
            existing.Status = updated.Status;
            existing.Summary = updated.Summary;
            existing.Title = updated.Title;
            existing.Text = updated.Text;
            existing.MetaDescription = updated.MetaDescription;
        });
    }

    public void Delete(int id)
    {
        repository.Delete(id);
    }
}