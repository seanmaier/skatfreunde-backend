using FluentValidation;
using skat_back.DTO.PlayerDTO;
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
            .WithMessage($"Name must be between {MinNameLength} and {MaxNameLength} characters long.");

        RuleFor(x => x.CreatedById)
            .NotEmpty()
            .WithMessage("UserId is required");
    }
}