using FluentValidation;
using skat_back.features.matchSessions.models;
using skat_back.utilities.validation.validators.matchRound;
using static skat_back.utilities.constants.TestingConstants;

namespace skat_back.utilities.validation.validators.matchSession;

public class CreateMatchSessionValidator: AbstractValidator<CreateMatchSessionDto>
{
    public CreateMatchSessionValidator()
    {
        RuleFor(x => x.CreatedByUserId)
            .NotEmpty()
            .WithMessage("UserId is required")
            .Must(BeValidGuid)
            .WithMessage("UserId must be a valid GUID.")
            .Must(guid => guid != TestUserId)
            .WithMessage("UserId must not be an empty GUID.");
        
        RuleFor(x => x.CalendarWeek)
            .NotEmpty()
            .WithMessage("CalendarWeek is required.")
            .Matches(@"^KW\d{2}$")
            .WithMessage("CalendarWeek must be in the format KWXX, where XX is a two-digit number.");

        RuleForEach(x => x.MatchRounds)
            .NotEmpty()
            .WithMessage("MatchRounds cannot be empty.")
            .SetValidator(new CreateMatchRoundValidator());
    }

    private bool BeValidGuid(string guid)
    {
        return Guid.TryParse(guid, out _);
    }
}