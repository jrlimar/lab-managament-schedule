using LabManagamentSchedule.Worker.Consumer.Consumers;
using RabbitMQ.Client;
using System.Threading;
using System.Threading.Tasks;

namespace LabManagamentSchedule.Worker.Consumer.ConsumerManager
{
    public class ConsumerManagement : IConsumerManagement
    {
        private readonly IConsumerCheckExam consumerCheckExam;
        private readonly IConsumerTeste consumerTeste;
        public ConsumerManagement(IConsumerCheckExam consumerCheckExam, IConsumerTeste consumerTeste)
        {
            this.consumerCheckExam = consumerCheckExam;
            this.consumerTeste = consumerTeste;
        }

        public async Task Inicialize()
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672, UserName = "guest", Password = "guest" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var manualResetEvent = new ManualResetEvent(false);
                manualResetEvent.Reset();

                await consumerCheckExam.Received(channel, manualResetEvent);
                await consumerTeste.Received(channel, manualResetEvent);

                manualResetEvent.WaitOne();
            }
        }
    }
}
