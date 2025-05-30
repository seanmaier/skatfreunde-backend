using FluentValidation;
using skat_back.features.players.models;
using static skat_back.utilities.constants.ValidationConstants;
using static skat_back.utilities.constants.TestingConstants;

namespace skat_back.utilities.validation.validators.players;

public class CreatePlayerValidator : AbstractValidator<CreatePlayerDto>
{
    public CreatePlayerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .Length(MinNameLength, MaxNameLength)
            .WithMessage($"Name must be between {MinNameLength} and {MaxNameLength} characters long.")
            .Matches("^[a-zA-Z0-9 ,]*$")
            .WithMessage("Name must only contain letters, numbers, commas and spaces.");

        RuleFor(x => x.CreatedById)
            .ValidateCreatedById();
    }
}