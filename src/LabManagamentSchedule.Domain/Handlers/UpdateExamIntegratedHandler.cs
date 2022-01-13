using LabManagamentSchedule.Core.Mediator;
using LabManagamentSchedule.Core.Messages.Notifications;
using LabManagamentSchedule.Domain.Commands;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LabManagamentSchedule.Domain.Handlers
{
    public class UpdateExamIntegratedHandler : IRequestHandler<UpdateExamIntegrationCommand, bool>
    {
        private readonly IMediatorHandler mediatorHandler;

        public UpdateExamIntegratedHandler(IMediatorHandler mediatorHandler)
        {
            this.mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(UpdateExamIntegrationCommand command, CancellationToken cancellationToken)
        {
            //publicar notificações
            if(!command.IsValid())
            {
                await mediatorHandler.PublishNotification(new Notification(command.Dominio, command.ValidationResult?.Errors.Select(x => x.ErrorMessage)));
            }

            //atualizar banco de dados

            return true;
        }
    }
}
