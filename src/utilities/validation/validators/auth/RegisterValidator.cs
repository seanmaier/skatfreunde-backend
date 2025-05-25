using FluentValidation;
using skat_back.features.auth.models;
using static skat_back.utilities.constants.ValidationConstants;

namespace skat_back.utilities.validation.validators.auth;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required.")
            .Length(MinUsernameLength, MaxUsernameLength)
            .WithMessage($"Username must be between {MinUsernameLength} and {MaxUsernameLength} characters long.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .ValidatePassword();
    }
}