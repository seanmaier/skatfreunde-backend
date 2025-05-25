using FluentValidation;
using skat_back.features.auth.models;

namespace skat_back.utilities.validation.validators.auth;

public class LoginValidator: AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.LoginInput)
            .NotEmpty()
            .WithMessage("Login input is required.")
            .Length(2, 50)
            .WithMessage("Login input must be between 2 and 50 characters long.");

        RuleFor(x => x.Password)
            .ValidatePassword();
    }
}