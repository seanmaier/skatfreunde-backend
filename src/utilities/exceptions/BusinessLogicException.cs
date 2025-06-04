namespace skat_back.utilities.exceptions;

public class BusinessLogicException(string userMessage, string errorCode = "BUSINESS_LOGIC_ERROR")
    : ApiException(userMessage)
{
    public override int StatusCode => StatusCodes.Status400BadRequest;
    public override string UserMessage { get; } = userMessage;
    public override string? ErrorCode { get; } = errorCode;
}