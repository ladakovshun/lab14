using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using WebApplication14.Exporters; // ������ ��� ������� �� CustomExporter

var builder = WebApplication.CreateBuilder(args);

// ��������� OpenTelemetry ��� ���� (tracing)
builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
            .AddAspNetCoreInstrumentation()  // �������������� ASP.NET Core
            .AddHttpClientInstrumentation()  // �������������� HTTP �볺���
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("WebApplication14Service"))  // ������ ���������� ��� �����
            .AddProcessor(new BatchActivityExportProcessor(new CustomExporter()));  // ������������� CustomExporter
    })
    .WithMetrics(metricsBuilder =>
    {
        metricsBuilder
            .AddAspNetCoreInstrumentation()  // �������������� ������ ��� ASP.NET Core
            .AddOtlpExporter(otlpOptions =>
            {
                otlpOptions.Endpoint = new Uri("http://localhost:4317"); // URL �� OTLP �������
            });
    });

builder.Services.AddControllersWithViews();

var app = builder.Build();

// �������� ������������ ��� �������������
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
