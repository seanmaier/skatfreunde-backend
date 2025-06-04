namespace skat_back.features.alertEmail;

public interface IAlertService
{
    Task ProcessErrorAsync(Exception ex, string traceId, AlertSeverity severity, string requestPath);
    Task SendWeeklyDigestAsync();
}