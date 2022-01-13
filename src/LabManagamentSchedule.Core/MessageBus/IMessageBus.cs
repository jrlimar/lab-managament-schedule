using LabManagamentSchedule.Core.Messages;
using RabbitMQ.Client;
using System.Threading.Tasks;

namespace LabManagamentSchedule.Core.MessageBus
{
    public interface IMessageBus
    {
        Task Publish<T>(T message, string queueName, string routeKey) where T : Event;
        Task Publish<T>(T message, string queueName, string exchangeName, string exchangeType, string routeKey) where T : Event;
        IBasicProperties CreeteBasicProperties<T>(IModel channel, T message) where T : Event;
    }
}
