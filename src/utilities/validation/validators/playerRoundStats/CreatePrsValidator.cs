using FluentValidation;
using skat_back.features.playerRoundStatistics.models;

namespace skat_back.utilities.validation.validators.playerRoundStats;

public class CreatePrsValidator : AbstractValidator<CreatePlayerRoundStatsDto>
{
    public CreatePrsValidator()
    {
        RuleFor(x => x.PlayerId)
            .NotEmpty()
            .WithMessage("PlayerId is required.");

        RuleFor(x => x.Points)
            .NotEmpty()
            .WithMessage("Points are required.")
            .GreaterThanOrEqualTo(-10_000)
            .WithMessage("Points must be greater than or equal to -10.000")
            .LessThan(10_000)
            .WithMessage("Points must be less than 10.000");

        RuleFor(x => x)
            .Must(dto => dto.Lost + dto.Won >= 0 && dto.Lost + dto.Won < 50)
            .WithMessage("The sum of won and lost games must be greater than 0 and less than 50.");
    }
}