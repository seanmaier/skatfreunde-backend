using System.Text;
using skat_back.features.email;

namespace skat_back.features.alertEmail;

public class AlertService: IAlertService
{
    private readonly IEmailService _emailService;
    private readonly ILogger<AlertService> _logger;
    private readonly Dictionary<string, AlertState> _alertStates = new();
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private readonly Timer _digestTimer;
    private readonly string _adminContact = Environment.GetEnvironmentVariable("ADMIN_EMAIL") ?? "admin@skatfreunde.de"; 
    
    public AlertService(IEmailService emailService, ILogger<AlertService> logger)
    {
        _emailService = emailService;
        _logger = logger;
        
        // Send digest every 10 minutes
        _digestTimer = new Timer(SendPendingDigests, null, TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(10));
    }
    
    public async Task ProcessErrorAsync(Exception ex, string traceId, AlertSeverity severity, string requestPath)
    {
        try
        {
            switch (severity)
            {
                case AlertSeverity.Critical:
                    await SendCriticalAlert(ex, traceId, requestPath);
                    break;
                    
                case AlertSeverity.Warning:
                    await ProcessWarningAlert(ex, traceId, requestPath);
                    break;
                    
                case AlertSeverity.Info:
                    // Just logged, no alert needed
                    break;
            }
        }
        catch (Exception alertEx)
        {
            _logger.LogError(alertEx, "Failed to process alert for exception {ExceptionType}", ex.GetType().Name);
        }
    }
    
    private async Task SendCriticalAlert(Exception ex, string traceId, string requestPath)
    {
        var subject = $"🚨 CRITICAL ERROR - {ex.GetType().Name}";
        var body = $"""
                    CRITICAL ERROR OCCURRED

                    Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC
                    TraceId: {traceId}
                    Path: {requestPath}
                    Exception: {ex.GetType().Name}
                    Message: {ex.Message}

                    This requires immediate attention!

                    Check logs with TraceId: {traceId}
                    """;

        await _emailService.SendEmailAsync(_adminContact, subject, body);
        _logger.LogInformation("Critical alert sent for TraceId: {TraceId}", traceId);
    }
    
    private async Task ProcessWarningAlert(Exception ex, string traceId, string requestPath)
    {
        await _semaphore.WaitAsync();
        try
        {
            var errorKey = GetErrorKey(ex);
            var alertState = GetOrCreateAlertState(errorKey);
            
            alertState.AddOccurrence(new ErrorOccurrence
            {
                TraceId = traceId,
                RequestPath = requestPath,
                Timestamp = DateTime.UtcNow,
                Message = ex.Message
            });
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private async void SendPendingDigests(object state)
    {
        await _semaphore.WaitAsync();
        try
        {
            var alertsToSend = _alertStates.Values
                .Where(a => a.HasPendingAlerts())
                .ToList();

            if (!alertsToSend.Any()) return;

            var subject = $"⚠️ Warning Digest - {alertsToSend.Count} Error Types";
            var body = BuildDigestBody(alertsToSend);

            await _emailService.SendEmailAsync(_adminContact, subject, body);
            
            // Clear sent alerts
            foreach (var alert in alertsToSend)
            {
                alert.ClearPendingAlerts();
            }

            _logger.LogInformation("Sent digest for {AlertCount} error types", alertsToSend.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send digest alerts");
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private string BuildDigestBody(List<AlertState> alerts)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Warning Digest - {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC");
        sb.AppendLine("=" + new string('=', 50));
        sb.AppendLine();

        foreach (var alert in alerts)
        {
            sb.AppendLine($"Error Type: {alert.ErrorType}");
            sb.AppendLine($"Occurrences: {alert.PendingOccurrences.Count}");
            sb.AppendLine($"First Seen: {alert.PendingOccurrences.First().Timestamp:HH:mm:ss}");
            sb.AppendLine($"Last Seen: {alert.PendingOccurrences.Last().Timestamp:HH:mm:ss}");
            sb.AppendLine($"Sample Message: {alert.PendingOccurrences.First().Message}");
            sb.AppendLine($"Sample TraceId: {alert.PendingOccurrences.First().TraceId}");
            sb.AppendLine("-" + new string('-', 30));
        }

        return sb.ToString();
    }

    private string GetErrorKey(Exception ex)
    {
        return $"{ex.GetType().Name}_{ex.Message.GetHashCode()}";
    }

    private AlertState GetOrCreateAlertState(string errorKey)
    {
        if (!_alertStates.TryGetValue(errorKey, out var state))
        {
            state = new AlertState(errorKey);
            _alertStates[errorKey] = state;
        }
        return state;
    }

    public Task SendDailyDigestAsync()
    {
        // Implementation for daily summary if needed
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _digestTimer?.Dispose();
        _semaphore?.Dispose();
    }
    
    
    public Task SendWeeklyDigestAsync()
    {
        throw new NotImplementedException();
    }
}