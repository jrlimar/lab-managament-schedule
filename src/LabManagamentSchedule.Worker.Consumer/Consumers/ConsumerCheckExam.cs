using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LabManagamentSchedule.Worker.Consumer.Consumers
{
    public class ConsumerCheckExam : IConsumerCheckExam
    {
        private readonly ILogger<ConsumerCheckExam> logger;
        const string queueName = "Check.Exams.Queue";
        public ConsumerCheckExam(ILogger<ConsumerCheckExam> logger)
        {
            this.logger = logger;
        }

        public async Task Received(IModel channel, ManualResetEvent manualResetEvent)
        {
            channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(channel); 
           
            consumer.Received += (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var json = JsonConvert.DeserializeObject<object>(Encoding.UTF8.GetString(body));
                    var dominio = ea.BasicProperties.Headers.Values.FirstOrDefault();

                    logger.LogWarning($"Fila: {queueName} - Consumer: {this.GetType().ToString()} - Domínio: {dominio = Encoding.UTF8.GetString((Byte[])dominio)}");

                    channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    logger.LogError($"{this.GetType().ToString()} - {ex.Message}");

                    channel.BasicNack(ea.DeliveryTag, false, true);
                    manualResetEvent.Set();
                }
            };

            channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

            await Task.CompletedTask;
        }
    }
}
