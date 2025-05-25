using Microsoft.AspNetCore.Identity;

namespace skat_back.utilities.exceptions;

public class PasswordResetFailedException(IEnumerable<IdentityError> errors)
    : Exception("Password reset failed. Check exception for more details.")
{
    public IEnumerable<IdentityError> Errors { get; } = errors;
}