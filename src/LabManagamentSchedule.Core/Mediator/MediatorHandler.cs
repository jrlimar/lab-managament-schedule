using LabManagamentSchedule.Core.Messages;
using LabManagamentSchedule.Core.Messages.LogEvents;
using LabManagamentSchedule.Core.Messages.Notifications;
using MediatR;
using System.Threading.Tasks;

namespace LabManagamentSchedule.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator mediator;

        public MediatorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task PublishEvent<T>(T @event) where T : Event
        {
            await mediator.Publish(@event);
        }

        public async Task PublishDomainEvent<T>(T notification) where T : Event
        {
            await mediator.Publish(notification);
        }

        public async Task<bool> SendCommand<T>(T command) where T : Command
        {
            return await mediator.Send(command);
        }

        public async Task PublishLogEvent<T>(T log) where T : LogEvent
        {
            await mediator.Publish(log);
        }

        public async Task PublishNotification<T>(T notification) where T : Notification
        {
            await mediator.Publish(notification);
        }
    }
}
