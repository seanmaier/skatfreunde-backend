namespace skat_back.utilities.exceptions;

public class EmailNotConfirmedException(string email) : Exception($"Email for user {email} has not been confirmed.")
{
}