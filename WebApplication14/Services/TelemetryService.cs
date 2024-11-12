using OpenTelemetry.Trace;

namespace WebApplication14.Services
{
    public class TelemetryService
    {
        private readonly Tracer _tracer;

        public TelemetryService(TracerProvider tracerProvider)
        {
            _tracer = tracerProvider.GetTracer("TelemetryServiceTracer");
        }

        public void ProcessTrace(string operation)
        {
            using (var span = _tracer.StartActiveSpan(operation))
            {
                span.SetAttribute("priority", "high");
                span.SetAttribute("status", "active");
               
            }
        }
    }
}