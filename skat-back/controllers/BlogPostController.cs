using skat_back.models;
using skat_back.services.BlogPostService;

namespace skat_back.controllers;

public class BlogPostController(BlogPostService service) : BaseController<BlogPost, BlogPostService>(service);