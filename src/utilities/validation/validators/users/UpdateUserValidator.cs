using FluentValidation;
using skat_back.features.user.models;
using static skat_back.utilities.constants.ValidationConstants;

namespace skat_back.utilities.validation.validators.users;

public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("User ID is required.");
        
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("First name is required.")
            .Length(MinNameLength, MaxNameLength)
            .WithMessage($"First name must be between {MinNameLength} and {MaxNameLength} characters long.");

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