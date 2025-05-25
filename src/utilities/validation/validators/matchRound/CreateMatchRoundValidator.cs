using FluentValidation;
using skat_back.features.matchRounds.models;
using skat_back.utilities.validation.validators.playerRoundStats;

namespace skat_back.utilities.validation.validators.matchRound;

public class CreateMatchRoundValidator: AbstractValidator<CreateMatchRoundDto>
{
    public CreateMatchRoundValidator()
    {
        RuleFor(x => x.Table)
            .NotEmpty()
            .WithMessage("Table cannot be empty.")
            .Length(3)
            .WithMessage("Table number must be exactly 3 characters long.");
        
        RuleFor(x => x.RoundNumber)
            .NotEmpty()
            .WithMessage("Round number cannot be empty.")
            .Length(1)
            .WithMessage("Round number must be exactly 1 character long.");

        RuleForEach(x => x.PlayerRoundStats)
            .NotEmpty()
            .WithMessage("Player round statistics cannot be empty.")
            .SetValidator(new CreatePrsValidator());
    }
}