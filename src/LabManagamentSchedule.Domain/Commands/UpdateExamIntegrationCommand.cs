using LabManagamentSchedule.Core.Messages;
using System.Collections.Generic;

namespace LabManagamentSchedule.Domain.Commands
{
    public class UpdateExamIntegrationCommand : Command
    {
        public IEnumerable<int> ResultsId { get; set; }
        public bool Integrated { get; set; }

        public UpdateExamIntegrationCommand(string dominio, IEnumerable<int> resultsId, bool integrated) 
            : base(dominio)
        {
            ResultsId = resultsId;
            Integrated = integrated;
        }

        public override bool IsValid()
        {
            //Criar UpdateExamIntegrationValidation
            return ValidationResult != null ? ValidationResult.IsValid : true;
        }
    }
}
