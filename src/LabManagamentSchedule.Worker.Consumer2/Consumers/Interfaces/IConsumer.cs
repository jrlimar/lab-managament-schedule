using System.Threading.Tasks;

namespace LabManagamentSchedule.Worker.Consumer2.Consumers
{
    public interface IConsumer
    {
        Task Received();
    }
}
