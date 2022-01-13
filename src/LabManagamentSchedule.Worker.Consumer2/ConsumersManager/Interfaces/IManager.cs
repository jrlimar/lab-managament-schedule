using RabbitMQ.Client;

namespace LabManagamentSchedule.Worker.Consumer2.ConsumersManager
{
    public interface IManager
    {
        IConnection GetConnection();
        IModel GetChannel();
    }
}
