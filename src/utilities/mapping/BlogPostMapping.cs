using skat_back.features.blogPosts.models;

namespace skat_back.utilities.mapping;

public static class BlogPostMapping
{
    public static BlogPost ToEntity(this CreateBlogPostDto entity)
    {
        return new BlogPost
        {
            Title = entity.Title,
            Text = entity.Text,
            Slug = entity.Slug,
            MetaTitle = entity.MetaTitle,
            MetaDescription = entity.MetaDescription,
            CreatedById = Guid.Parse(entity.CreatedById)
        };
    }

    public static BlogPost ToEntity(this UpdateBlogPostDto entity)
    {
        return new BlogPost
        {
            CreatedById = Guid.Parse(entity.CreatedById),
            Title = entity.Title,
            Text = entity.Text,
            Slug = entity.Slug,
            MetaTitle = entity.MetaTitle,
            MetaDescription = entity.MetaDescription
        };
    }

    public static ResponseBlogPostDto ToDto(this BlogPost entity)
    {
        return new ResponseBlogPostDto(
            entity.Id,
            entity.CreatedById.ToString(),
            entity.Title,
            entity.Text,
            entity.Slug,
            entity.Summary,
            entity.Status,
            entity.MetaTitle,
            entity.MetaDescription,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
}