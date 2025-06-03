using skat_back.features.blogPosts.models;
using skat_back.Lib;

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
            CreatedById = Guid.Parse(entity.CreatedById),
        };
    }

    public static BlogPost ToEntity(this UpdateBlogPostDto entity)
    {
        return new BlogPost
        {
            UpdatedById = Guid.Parse(entity.UpdatedById),
            Title = entity.Title,
            Text = entity.Text,
            Slug = entity.Slug,
            MetaTitle = entity.MetaTitle,
            MetaDescription = entity.MetaDescription,
        };
    }

    public static PagedResult<ResponseBlogPostDto> ToPagedResult(this PagedResult<BlogPost> pagedResult)
    {
        return new PagedResult<ResponseBlogPostDto>(
            pagedResult.Data.Select(x => x.ToResponse()).ToList(),
            pagedResult.PageNumber,
            pagedResult.PageSize,
            pagedResult.TotalCount
        );
    }
    
    public static ResponseBlogPostDto ToResponse(this BlogPost entity)
    {
        return new ResponseBlogPostDto(
            entity.Id,
            entity.Title,
            entity.Text,
            entity.Slug,
            entity.Summary,
            entity.Status,
            entity.MetaTitle,
            entity.MetaDescription,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.CreatedById.ToString(),
            entity.UpdatedById.ToString()
        );
    }
}