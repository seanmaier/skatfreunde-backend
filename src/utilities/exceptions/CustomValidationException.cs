namespace skat_back.utilities.exceptions;

public class CustomValidationException(Dictionary<string, string[]> errors) : ApiException("Validation failed")
{
    public override int StatusCode => StatusCodes.Status400BadRequest;
    public override string UserMessage => "One or more validation errors occurred";
    public override string ErrorCode => "VALIDATION_ERROR";
    public Dictionary<string, string[]> ValidationErrors { get; } = errors;
}