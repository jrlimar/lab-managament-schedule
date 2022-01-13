using FluentValidation.Results;
using MediatR;
using System;

namespace LabManagamentSchedule.Core.Messages
{
    public abstract class Command : Message, IRequest<bool>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command(string dominio) 
            : base(dominio)
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }
}
