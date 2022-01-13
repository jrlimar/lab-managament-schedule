using LabManagamentSchedule.Core.Messages.Notifications;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LabManagamentSchedule.Domain.Handlers
{
    public class NotificationHandler : INotificationHandler<Notification>
    {
        private List<Notification> notifications;

        public NotificationHandler()
        {
            this.notifications = new List<Notification>();
        }

        public Task Handle(Notification notification, CancellationToken cancellationToken)
        {
            notifications.Add(notification);

            return Task.CompletedTask;
        }

        public virtual IList<Notification> GetNotifications()
        {
            return notifications;
        }

        public virtual bool HasNotification()
        {
            return notifications.Any();
        }

        public void Dispose()
        {
            //notifications.Clear();
            notifications = new List<Notification>();
        }
    }
}
