using LabManagamentSchedule.Core.Messages.LogEvents;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace LabManagamentSchedule.Domain.Events.Logs
{
    public class LogEventHandler : INotificationHandler<LogEvent>
    {
        private readonly ILogger<LogEventHandler> logger;
        public LogEventHandler(ILogger<LogEventHandler> logger)
        {
            this.logger = logger;
        }

        public async Task Handle(LogEvent log, CancellationToken cancellationToken)
        {
            //enviar para algum lugar, persistir, enviar email ...
            logger.LogError(log.ToJson());

            await Task.CompletedTask;
        }
    }
}
