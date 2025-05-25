namespace skat_back.utilities.exceptions;

public class EmailNotFoundException(string email) : Exception($"No user found with email: {email}")
{
}