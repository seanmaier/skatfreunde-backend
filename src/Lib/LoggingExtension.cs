namespace skat_back.Lib;

public static class LoggingExtension
{
    public static void LogSecurityEvent(this ILogger logger, string eventType, string details, string? userId = null)
    {
        logger.LogWarning("Security Event: {EventType} - {Details} - User: {UserId}", eventType, details,
            userId ?? "Anonymous");
    }

    public static void LogBusinessError(this ILogger logger, Exception ex, string operation, object? context = null)
    {
        logger.LogError(ex, "Business operation failed: {Operation} - Context: {@Context}", operation, context);
    }

    public static void LogUnexpectedError(this ILogger logger, Exception ex, string operation, string traceId)
    {
        logger.LogError(ex, "Unexpected error in {Operation} - TraceId: {TraceId} - Type: {ExceptionType}",
            operation, traceId, ex.GetType().Name);
    }
}