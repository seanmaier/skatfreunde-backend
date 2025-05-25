using FluentValidation;
using skat_back.features.auth.models;
using static skat_back.utilities.constants.ValidationConstants;

namespace skat_back.utilities.validation.validators.auth;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Username)
            .Length(MinUsernameLength, MaxUsernameLength)
            .WithMessage($"Username must be between {MinUsernameLength} and {MaxUsernameLength} characters long.")
            .When(x => string.IsNullOrEmpty(x.Email))
            .NotEmpty()
            .WithMessage("Username is required.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .DependentRules(() =>
            {
                RuleFor(x => x.Email)
                    .EmailAddress()
                    .WithMessage("Invalid email format.");
            });

        RuleFor(x => x.Password)
            .ValidatePassword();
    }
}