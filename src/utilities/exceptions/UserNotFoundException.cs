namespace skat_back.utilities.exceptions;

public class UserNotFoundException(string identifier)
    : Exception($"No user found with email: {identifier} or userId: {identifier}")
{
    public UserNotFoundException(string email, string userId) : this($"{email} or {userId}")
    {
    }
}