using RabbitMQ.Client;

namespace LabManagamentSchedule.Worker.Consumer2.ConsumersManager
{
    public class Manager : IManager
    {
        private readonly IConnection connection;
        private readonly IModel channel;

        public Manager()
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
