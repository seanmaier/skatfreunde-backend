using FluentValidation;
using skat_back.features.auth.models;

namespace skat_back.utilities.validation.validators.users;

public class CreateUserValidator : AbstractValidator<RegisterDto>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("First name is required.")
            .Length(2, 50)
            .WithMessage("First name must be between 2 and 50 characters long.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email address format.");
    }
}