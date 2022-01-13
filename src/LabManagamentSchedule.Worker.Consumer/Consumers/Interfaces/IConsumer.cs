using RabbitMQ.Client;
using System.Threading;
using System.Threading.Tasks;

namespace LabManagamentSchedule.Worker.Consumer.Consumers
{
    public interface IConsumer
    {
        Task Received(IModel channel, ManualResetEvent manualResetEvent);
    }
}
