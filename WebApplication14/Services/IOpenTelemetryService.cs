using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;

public interface IOpenTelemetryService
{
    void ConfigureTracing(TracerProviderBuilder tracerProviderBuilder);
    void ConfigureMetrics(MeterProviderBuilder meterProviderBuilder);
}
