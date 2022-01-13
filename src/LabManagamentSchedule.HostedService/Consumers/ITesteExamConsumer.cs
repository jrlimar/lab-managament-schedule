using System.Threading.Tasks;

namespace LabManagamentSchedule.HostedService.Consumers
{
    public interface ITesteExamConsumer
    {
        Task Receive();
    }
}
