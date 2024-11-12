using OpenTelemetry;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Instrumentation.SqlClient;
using WebApplication14.Exporters; 

public class OpenTelemetryService : IOpenTelemetryService
{
    public void ConfigureTracing(TracerProviderBuilder tracerProviderBuilder)
    {
        tracerProviderBuilder
            .AddAspNetCoreInstrumentation()   // Інструментація для ASP.NET Core
            .AddHttpClientInstrumentation()   // Інструментація для HTTP клієнта
            .AddSqlClientInstrumentation()    // Інструментація для SQL клієнтів (потрібен пакет OpenTelemetry.Instrumentation.SqlClient)
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("WebApplication14Service"))
            .AddProcessor(new BatchActivityExportProcessor(new CustomExporter())); // Експортер (за потребою)
    }

    public void ConfigureMetrics(MeterProviderBuilder meterProviderBuilder)
    {
        meterProviderBuilder
            .AddAspNetCoreInstrumentation()   // Інструментація для ASP.NET Core
            .AddHttpClientInstrumentation()   // Інструментація для HTTP клієнта
            .AddOtlpExporter(otlpOptions =>
            {
                otlpOptions.Endpoint = new Uri("http://localhost:4317"); // URL OpenTelemetry Collector
            });
    }
}

