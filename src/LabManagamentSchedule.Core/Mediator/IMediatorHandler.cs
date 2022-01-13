using LabManagamentSchedule.Core.Messages;
using LabManagamentSchedule.Core.Messages.LogEvents;
using LabManagamentSchedule.Core.Messages.Notifications;
using System.Threading.Tasks;

namespace LabManagamentSchedule.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;
        Task PublishDomainEvent<T>(T notificacao) where T : Event;
        Task<bool> SendCommand<T>(T command) where T : Command;
        Task PublishLogEvent<T>(T log) where T : LogEvent;
        Task PublishNotification<T>(T notification) where T : Notification;
    }
}
