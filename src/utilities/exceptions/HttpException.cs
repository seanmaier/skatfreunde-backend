namespace skat_back.utilities.exceptions;

public class HttpException(int statusCode, string message) : Exception
{
    public int StatusCode { get; } = statusCode;
}