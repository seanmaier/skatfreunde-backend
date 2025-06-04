namespace skat_back.features.alertEmail;

public class AlertState
{
    public string ErrorType { get; }
    public List<ErrorOccurrence> PendingOccurrences { get; } = new();
    public DateTime LastDigestSent { get; private set; } = DateTime.UtcNow;

    public AlertState(string errorType)
    {
        ErrorType = errorType;
    }

    public void AddOccurrence(ErrorOccurrence occurrence)
    {
        PendingOccurrences.Add(occurrence);
    }

    public bool HasPendingAlerts()
    {
        return PendingOccurrences.Any();
    }

    public void ClearPendingAlerts()
    {
        PendingOccurrences.Clear();
        LastDigestSent = DateTime.UtcNow;
    }
}