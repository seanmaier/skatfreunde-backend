using FluentValidation;
using skat_back.features.user.models;
using static skat_back.utilities.constants.ValidationConstants;

namespace skat_back.utilities.validation.validators.users;

public class CreateUserValidator: AbstractValidator<CreateUserDto>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required.")
            .Length(MinNameLength, MaxNameLength)
            .WithMessage($"Username must be between {MinNameLength} and {MaxNameLength} characters long.");

        RuleFor(x => x.Password)
            .ValidatePassword();

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email address format.");

        RuleFor(x => x.Role)
            .NotEmpty()
            .WithMessage("Role is required.");
    }
}