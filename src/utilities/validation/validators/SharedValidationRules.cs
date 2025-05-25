using FluentValidation;
using static skat_back.utilities.constants.TestingConstants;

namespace skat_back.utilities.validation.validators;

public static class SharedValidationRules
{
    public static IRuleBuilder<T, string> ValidateCreatedById<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage("UserId is required")
            .Must(BeValidGuid)
            .WithMessage("UserId must be a valid GUID.")
            .Must(guid => guid != TestUserId)
            .WithMessage("UserId must not be an empty GUID.");
    }

    private static bool BeValidGuid(string guid)
    {
        return Guid.TryParse(guid, out _);
    }
}