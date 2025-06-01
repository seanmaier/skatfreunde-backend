using FluentValidation;
using skat_back.features.matches.matchSessions.models;
using skat_back.utilities.validation.validators.matchRound;
using static skat_back.utilities.constants.TestingConstants;

namespace skat_back.utilities.validation.validators.matchSession;

public class CreateMatchSessionValidator : AbstractValidator<CreateMatchSessionDto>
{
    public CreateMatchSessionValidator()
    {
        RuleFor(x => x.CreatedById)
            .NotEmpty()
            .WithMessage("UserId is required")
            .Must(BeValidGuid)
            .WithMessage("UserId must be a valid GUID.")
            .Must(guid => guid != TestUserId)
            .WithMessage("UserId must not be an empty GUID.");

        RuleFor(x => x.PlayedAt)
            .NotEmpty()
            .WithMessage("PlayedAt is required.")
            .Matches(@"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}(?:\.\d+)?(?:Z|[+-]\d{2}:\d{2})$")
            .WithMessage("PlayedAt must be a valid ISO 8601 time string.");

        RuleFor(x => x.MatchRounds)
            .NotEmpty()
            .WithMessage("MatchRounds cannot be empty.")
            /*.Must(matchRound => matchRound
                .GroupBy(round => round.RoundNumber)
                .All(group => group.Count() == 1))
            .WithMessage("Each MatchRound's RoundNumber must be unique within the MatchSession.")*/;

        RuleForEach(x => x.MatchRounds)
            .NotEmpty()
            .SetValidator(new CreateMatchRoundValidator());
    }

    private bool BeValidGuid(string guid)
    {
        return Guid.TryParse(guid, out _);
    }
}