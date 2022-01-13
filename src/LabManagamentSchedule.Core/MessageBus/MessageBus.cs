using LabManagamentSchedule.Core.AppSettings;
using LabManagamentSchedule.Core.Messages;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LabManagamentSchedule.Core.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private readonly ILogger<MessageBus> logger;
        private readonly RabbitSettings settings;
        public MessageBus(RabbitSettings settings, ILogger<MessageBus> logger)
        {
            this.settings = settings;
            this.logger = logger;
        }

        public async Task Publish<T>(T message, string queueName, string routeKey) where T : Event
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = settings.HostName, Port = settings.Port, UserName = settings.UserName, Password = settings.Password};

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                    var body = Encoding.UTF8.GetBytes(message.ToJson());
                    var basicProperties = CreeteBasicProperties(channel, message);

                    channel.BasicPublish("", queueName, basicProperties, body);

                    logger.LogDebug($"FINALIZADO Fila: {queueName} - Mensagem: {message.MessageType} - Domínio: {message.Dominio} as {DateTimeOffset.Now}");
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"ERRO Fila: {queueName} - Domínio: {message.Dominio} - {ex.Message}");
            }

            await Task.CompletedTask;
        }

        public async Task Publish<T>(T message, string queueName, string exchangeName, string exchangeType, string routeKey) where T : Event
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = settings.HostName, Port = settings.Port, UserName = settings.UserName, Password = settings.Password };

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel()) 
                {
                    channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                    channel.ExchangeDeclare(exchangeName, exchangeType);
                    channel.QueueBind(queueName, exchangeName, "");

                    var body = Encoding.UTF8.GetBytes(message.ToJson());
                    var basicProperties = CreeteBasicProperties(channel, message);

                    channel.BasicPublish(exchangeName, routeKey, basicProperties, body);

                    logger.LogDebug($"FINALIZADO Fila: {queueName} - Mensagem: {message.MessageType} - Domínio: {message.Dominio} as {DateTimeOffset.Now}");
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"ERRO Fila: {queueName} - Domínio: {message.Dominio} - {ex.Message}");
            }

            await Task.CompletedTask;
        }

        public IBasicProperties CreeteBasicProperties<T>(IModel channel, T message) where T : Event
        {
            var basicProperties = channel.CreateBasicProperties();
            basicProperties.ContentType = "application/json";
            basicProperties.DeliveryMode = 2;
            basicProperties.Persistent = true;
            basicProperties.Headers = new Dictionary<string, object>();
            basicProperties.Headers.Add("dominio", message.Dominio);

            return basicProperties;
        }
    }
}
