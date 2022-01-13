using System.Threading.Tasks;

namespace LabManagamentSchedule.HostedService.Consumers
{
    public interface ISignedExamConsumer
    {
        Task Receive();
    }
}
