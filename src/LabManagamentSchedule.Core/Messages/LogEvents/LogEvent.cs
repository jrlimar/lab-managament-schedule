using System;

namespace LabManagamentSchedule.Core.Messages.LogEvents
{
    public class LogEvent : Event
    {
        public string Log { get; private set; }
        public DateTime Timestamp { get; private set; }

        public LogEvent(string dominio, string log) 
            : base(dominio)
        {
            Log = log;
            Timestamp = DateTime.Now;
        }
    }
}
