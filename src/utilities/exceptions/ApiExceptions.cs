namespace skat_back.utilities.exceptions;

public abstract class ApiException : Exception
{
    protected ApiException(string message) : base(message)
    {
    }

    protected ApiException(string message, Exception? innerException) : base(message, innerException)
    {
    }

    public abstract int StatusCode { get; }
    public abstract string UserMessage { get; }
    public virtual string? ErrorCode { get; }
}