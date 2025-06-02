using FluentValidation;
using skat_back.features.statistics.models;

namespace skat_back.utilities.validation.validators.statistics;

public class MatchSessionQueryValidator : AbstractValidator<MatchSessionQuery>
{
    public MatchSessionQueryValidator()
    {
        RuleFor(query => query.WeekStart)
            .NotEmpty()
            .WithMessage("Calendar week start date must not be empty.")
            .Must(BeAValidDate)
            .WithMessage("The value provided for WeekStart is not a valid date.");
    }

    private static bool BeAValidDate(DateTime date)
    {
        return date.Kind == DateTimeKind.Utc; // Ensure the date is in UTC
    }
}