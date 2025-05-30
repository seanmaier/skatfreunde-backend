using FluentValidation;
using skat_back.features.blogPosts.models;
using static skat_back.utilities.constants.ValidationConstants;

namespace skat_back.utilities.validation.validators.blogposts;

public class UpdateBlogPostValidator: AbstractValidator<UpdateBlogPostDto>
{
    public UpdateBlogPostValidator()
    {
        RuleFor(x => x.UpdatedById)
            .ValidateCreatedById();
        
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .Length(MinCharLength, MaxTitleLength)
            .WithMessage("Title must be between 1 and 100 characters long.");
        
        RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage("Text is required.")
            .Length(MinCharLength, MaxTextLength)
            .WithMessage("Text must be between 1 and 5000 characters long.");

        RuleFor(x => x.Status)
            .NotEmpty()
            .WithMessage("Status is required.")
            .Must(x => Enum.IsDefined(typeof(BlogStatus), x))
            .WithMessage("Invalid blog post status.");
    }
}