using Serilog.Core;
using Serilog.Events;

namespace skat_back.features.correlation;

public class CorrelationEnricher(IServiceProvider serviceProvider) : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        using var scope = serviceProvider.CreateScope();
        var correlationService = scope.ServiceProvider.GetService<CorrelationService>();

        if (correlationService == null) return;

        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("TraceId", correlationService.TraceId));
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("UserId", correlationService.SafeUserId));
    }
}