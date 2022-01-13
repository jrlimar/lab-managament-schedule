using LabManagamentSchedule.Core.MessageBus;
using LabManagamentSchedule.Core.MessageBus.Queues;
using MediatR;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LabManagamentSchedule.Domain.Events
{
    public class CheckExamHandler : INotificationHandler<CheckExamEvent>
    {
        private readonly IMessageBus bus;
        private readonly ILogger<CheckExamHandler> logger;
        private readonly CkeckExamQueue queue;
        public CheckExamHandler(ILogger<CheckExamHandler> logger, IMessageBus bus, CkeckExamQueue queue)
        {
            this.logger = logger;
            this.bus = bus;
            this.queue = queue;
        }

        public async Task Handle(CheckExamEvent @event, CancellationToken cancellationToken)
        {
            logger.LogDebug($"PUBLICANDO Fila {queue.QueueName} - Domínio: {@event.Dominio} - Mensagem {@event.MessageType}, início as {DateTimeOffset.Now}");

            await bus.Publish(@event, queue.QueueName, queue.Exchange, ExchangeType.Fanout, "");
        }
    }
}
