using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using WebApplication14.Exporters; // Додано для доступу до CustomExporter

var builder = WebApplication.CreateBuilder(args);

// Додавання OpenTelemetry для слідів (tracing)
builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
            .AddAspNetCoreInstrumentation()  // Інструментація ASP.NET Core
            .AddHttpClientInstrumentation()  // Інструментація HTTP клієнтів
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("WebApplication14Service"))  // Додаємо інформацію про сервіс
            .AddProcessor(new BatchActivityExportProcessor(new CustomExporter()));  // Використовуємо CustomExporter
    })
    .WithMetrics(metricsBuilder =>
    {
        metricsBuilder
            .AddAspNetCoreInstrumentation()  // Інструментація метрик для ASP.NET Core
            .AddOtlpExporter(otlpOptions =>
            {
                otlpOptions.Endpoint = new Uri("http://localhost:4317"); // URL до OTLP сервера
            });
    });

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Додаткові налаштування для маршрутизації
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
