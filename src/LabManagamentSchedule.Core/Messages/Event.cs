using MediatR;
using Newtonsoft.Json;
using System;

namespace LabManagamentSchedule.Core.Messages
{
    public abstract class Event : Message, INotification
    {
        public DateTime TimeStamp { get; protected set; }

        protected Event(string dominio)
            : base(dominio)
        {
            this.TimeStamp = DateTime.Now;
        }
    }
}
