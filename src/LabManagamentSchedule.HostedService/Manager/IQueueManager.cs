using RabbitMQ.Client;

namespace LabManagamentSchedule.HostedService.Manager
{
    public interface IQueueManager
    {
        IConnection GetConnection();
        IModel GetChannel();
    }
}
