namespace skat_back.features.alertEmail;

public class ErrorOccurrence
{
    public string TraceId { get; set; }
    public string RequestPath { get; set; }
    public DateTime Timestamp { get; set; }
    public string Message { get; set; }
}