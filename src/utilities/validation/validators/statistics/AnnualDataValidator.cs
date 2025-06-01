using FluentValidation;
using skat_back.features.statistics.models;

namespace skat_back.utilities.validation.validators.statistics;

public class AnnualDataValidator : AbstractValidator<AnnualDataQuery>
{
    public AnnualDataValidator()
    {
        RuleFor(query => query.RequestYear)
            .NotEmpty()
            .WithMessage("Request year cannot be empty.")
            .DependentRules(() =>
            {
                RuleFor(query => query.RequestYear).GreaterThanOrEqualTo(new DateTime(2000, 1, 1))
                    .WithMessage("Year must be after January 1, 2000.")
                    .LessThanOrEqualTo(DateTime.Now.AddMinutes(10))
                    .WithMessage("Year cannot be in the future.");
            });
    }
}