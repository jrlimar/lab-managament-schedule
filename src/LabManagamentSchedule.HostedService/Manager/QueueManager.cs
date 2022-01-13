using RabbitMQ.Client;

namespace LabManagamentSchedule.HostedService.Manager
{
    public class QueueManager : IQueueManager
    {
        private readonly IConnection connection;
        private readonly IModel channel;

        public QueueManager()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory() { HostName = "localhost", Port = 5672, UserName = "guest", Password = "guest" };
            connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
        }

        public IConnection GetConnection()
        {
            return connection;
        }

        public IModel GetChannel()
        {
            return channel;
        }
    }
}
