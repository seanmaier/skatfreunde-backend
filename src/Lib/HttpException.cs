namespace skat_back.Lib;

public class HttpException(int statusCode, string message) : Exception
{
    public int StatusCode { get; } = statusCode;
}