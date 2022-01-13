using LabManagamentSchedule.Worker.Consumer2.ConsumersManager;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabManagamentSchedule.Worker.Consumer2.Consumers
{
    public class CheckExamConsumer : ICheckExamConsumer
    {
        private readonly IManager manager;
        private readonly ILogger<CheckExamConsumer> logger;
        const string queueName = "Check.Exams.Queue";

        public CheckExamConsumer(IManager manager, ILogger<CheckExamConsumer> logger)
        {
            this.manager = manager;
            this.logger = logger;
        }

        public async Task Received()
        {
            var channel = manager.GetChannel();
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
                    channel.BasicNack(ea.DeliveryTag, false, true);
                }
            };

            channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

            await Task.CompletedTask;
        }
    }
}
