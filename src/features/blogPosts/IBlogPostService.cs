using skat_back.features.blogPosts;
using skat_back.features.blogPosts.models;
using skat_back.Lib;

namespace skat_back.Features.BlogPosts;

/// <summary>
///     Represents the service interface for managing blog posts.
///     Specific implementations for the BlogPost Service should be provided here.
/// </summary>
public interface IBlogPostService : IService<ResponseBlogPostDto, CreateBlogPostDto, UpdateBlogPostDto, int>
{
}