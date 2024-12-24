using Microsoft.EntityFrameworkCore;
using skat_back.controllers;
using skat_back.data;

namespace skat_back.services;

public class BlogPostService: IService<BlogPost>
{
    private readonly IRepository<BlogPost> _repository;

    public BlogPostService(IRepository<BlogPost> repository)
    {
        _repository = repository;
    }

    public BlogPost? GetById(int id)
    {
        return _repository.GetById(id);
    }

    public IEnumerable<BlogPost> GetAll()
    {
        return _repository.GetAll();
    }

    public void Add(BlogPost blogPost)
    {
        _repository.Add(blogPost);
    }

    public void Update(int id, BlogPost updatedBlogPost)
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

    public void Delete(int id)
    {
        _repository.Delete(id);
    }
}