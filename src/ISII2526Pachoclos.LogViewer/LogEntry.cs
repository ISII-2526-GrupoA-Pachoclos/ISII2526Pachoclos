
using System;

namespace AppForSEII2526.API.ISII2526Pachoclos.LogViewer
{
    public class LogEntry
    {
        public DateTime Timestamp { get; set; }
        
        public string LogLevel { get; set; }
        public string Category { get; set; }
        public int EventId { get; set; }
        public string? EventName { get; set; }
        public string Message { get; set; }
        public string? Exception { get; set; }


       
    }
}
