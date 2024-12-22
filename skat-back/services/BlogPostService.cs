using Microsoft.EntityFrameworkCore;
using skat_back.data;

namespace skat_back.services;

public class BlogPostService
{
    private readonly Repository<BlogPost> _repository;

    public BlogPostService(Repository<BlogPost> repository)
    {
        _repository = repository;
    }

    public BlogPost? GetBlogPost(int id)
    {
        return _repository.GetById(id);
    }

    public IEnumerable<BlogPost> GetAllBlogPosts()
    {
        return _repository.GetAll();
    }

    public void AddBlogPost(BlogPost blogPost)
    {
        _repository.Add(blogPost);
    }

    public void UpdateBlogPost(BlogPost updatedBlogPost, int id)
    {
        _repository.Update(id, updatedBlogPost, (existing, updated) =>
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

    public void DeleteBlogPost(int id)
    {
        _repository.Delete(id);
    }
}