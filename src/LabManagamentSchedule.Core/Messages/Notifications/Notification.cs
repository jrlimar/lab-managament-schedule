using MediatR;
using System.Collections.Generic;

namespace LabManagamentSchedule.Core.Messages.Notifications
{
    public class Notification : Message, INotification
    {
        public string Error { get; private set; }
        public IEnumerable<string> Errors { get; private set; }

        public Notification(string dominio, string error)
            : base(dominio)
        {
            Error = error;
        }

        public Notification(string dominio, IEnumerable<string> errors)
           : base(dominio)
        {
            Errors = errors;
        }
    }
}
