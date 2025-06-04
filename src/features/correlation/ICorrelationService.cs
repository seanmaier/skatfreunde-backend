namespace skat_back.features.correlation;

public interface ICorrelationService
{
    string TraceId { get; }
    string SafeUserId { get; }
}