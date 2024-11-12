using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Trace;
using System.Diagnostics;
using WebApplication14.Models;

namespace WebApplication14.Controllers
{
    public class HomeController : Controller
    {
        private readonly Tracer _tracer;

        public HomeController(TracerProvider tracerProvider)
        {
            _tracer = tracerProvider.GetTracer("OpenTelemetryDemoTracer");
        }

        public IActionResult Index()
        {
            using (var span = _tracer.StartActiveSpan("HomeController.Index"))
            {
                span.SetAttribute("custom-attribute", "sample-value");
 
            }

            var traceData = new TraceData
            {
                OperationName = "HomeController.Index",
                StartTime = DateTime.UtcNow,
                Duration = TimeSpan.FromMilliseconds(150), // Приклад тривалості
                Status = "Success"
            };

            return View(traceData);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}