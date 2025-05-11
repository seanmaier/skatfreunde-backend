using FluentValidation;
using skat_back.Features.Players;
using static skat_back.utilities.constants.ValidationConstants;

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
            .Matches("^[a-zA-Z0-9 ]*$")
            .WithMessage("Name must only contain letters, numbers, and spaces.");

        RuleFor(x => x.CreatedByUserId)
            .NotEmpty()
            .WithMessage("UserId is required")
            .Must(BeValidGuid)
            .WithMessage("UserId must be a valid GUID.")
            .Must(guid => guid != "00000000-0000-0000-0000-000000000000")
            .WithMessage("UserId must not be an empty GUID.");
    }

    private bool BeValidGuid(string guid)
    {
        return Guid.TryParse(guid, out _);
    }
}