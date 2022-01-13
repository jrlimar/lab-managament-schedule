using LabManagamentSchedule.Core.Messages;
using System.Collections.Generic;

namespace LabManagamentSchedule.Domain.Events
{
    public class CheckExamEvent : Event
    {
        public IEnumerable<int> ResultsId { get; private set; }
        public int ResultId { get; set; }

        public CheckExamEvent(string dominio, IEnumerable<int> resultsId)
            : base(dominio)
        {
            ResultsId = resultsId;
        }

        public CheckExamEvent(string dominio,  int resultId)
           : base(dominio)
        {
            ResultId = resultId;
        }
    }
}
