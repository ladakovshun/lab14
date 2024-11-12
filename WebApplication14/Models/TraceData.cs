namespace WebApplication14.Models
{
    public class TraceData
    {
        public string OperationName { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string Status { get; set; }
    }
}