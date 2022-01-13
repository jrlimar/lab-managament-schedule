using System;

namespace LabManagamentSchedule.Core.Messages.DomainEvents
{
    public class DomainEvent : Event
    {
        public DateTime Timestamp { get; private set; }

        protected DomainEvent(string dominio) 
            : base(dominio)
        {
            Timestamp = DateTime.Now;
        }
    }
}
