using FluentValidation;
using skat_back.features.matchRounds.models;
using skat_back.utilities.validation.validators.playerRoundStats;

namespace skat_back.utilities.validation.validators.matchRound;

public class CreateMatchRoundValidator : AbstractValidator<CreateMatchRoundDto>
{
    public CreateMatchRoundValidator()
    {
        RuleFor(x => x.Table)
            .NotEmpty()
            .WithMessage("Table cannot be empty.")
            .Length(1, 2)
            .WithMessage("Table number must be between 1 and 2 characters long.");

        RuleFor(x => x.RoundNumber)
            .NotEmpty()
            .WithMessage("Round number cannot be empty.")
            .Length(1)
            .WithMessage("Round number must be exactly 1 character long.");

        RuleFor(x => x.PlayerRoundStats)
            .NotEmpty()
            .WithMessage("Player round statistics cannot be empty.")
            .Must(matchRound => matchRound
                .GroupBy(playerRoundStats => playerRoundStats.PlayerId)
                .All(group => group.Count() == 1))
            .WithMessage("Player round statistics must have unique player IDs.");

        RuleForEach(x => x.PlayerRoundStats)
            .SetValidator(new CreatePrsValidator());
    }
}