namespace skat_back.utilities.exceptions;

public class ResourceNotFoundException(string resourceType, object id)
    : ApiException($"{resourceType} with ID {id} was not found")
{
    public override int StatusCode => StatusCodes.Status404NotFound;
    public override string UserMessage => "The requested resource was not found";
    public override string ErrorCode => "RESOURCE_NOT_FOUND";
}