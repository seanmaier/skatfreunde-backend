namespace skat_back.Lib;

public class ErrorResponse
{
    public int Status { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Detail { get; set; }
    public string? ErrorCode { get; set; }
    public string TraceId { get; set; } = string.Empty;
    public Dictionary<string, string[]> ValidationErrors { get; set; }

    // Development only
    public string? StackTrace { get; set; }
    public string? DeveloperMessage { get; set; }
}