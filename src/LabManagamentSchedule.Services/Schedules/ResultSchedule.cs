using LabManagamentSchedule.Core.Mediator;
using LabManagamentSchedule.Core.Messages.LogEvents;
using LabManagamentSchedule.Domain.Commands;
using LabManagamentSchedule.Domain.Events;
using LabManagamentSchedule.Services.Gateways;
using LabManagamentSchedule.Services.Security;
using ManagerExamsLabs.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LabManagamentSchedule.Services.Schedules
{
    public class ResultSchedule : IResultSchedule
    {
        private readonly IDominioGateway dominioGateway;
        private readonly ICryptographyService cryptographyService;
        private readonly IResultRepository resultRepository;
        private readonly IMediatorHandler mediatorHandler;
        private readonly ILogger<ResultSchedule> logger;

        public ResultSchedule(IDominioGateway dominioGateway, IResultRepository resultRepository, IMediatorHandler mediatorHandler, ILogger<ResultSchedule> logger, ICryptographyService cryptographyService)
        {
            this.dominioGateway = dominioGateway;
            this.resultRepository = resultRepository;
            this.mediatorHandler = mediatorHandler;
            this.logger = logger;
            this.cryptographyService = cryptographyService;
        }

        public async Task ExamsCheckedsFromDomain()
        {
            var dominios = await dominioGateway.GetDomains();

            foreach (var dominio in dominios)
            {
                try
                {
                    resultRepository.CreateConnection(cryptographyService.Decrypt(dominio.ConnectionString));

                    var results = await resultRepository.GetExamsCheckedsFromDomain();
                    var message = new CheckExamEvent(dominio.Id, results);

                    logger.LogDebug($"PROCESSAMENTO do Domínio: {dominio.Id} - Mensagem {message.MessageType}, início as {DateTimeOffset.Now}");

                    await mediatorHandler.PublishEvent(message);
                    await mediatorHandler.SendCommand(new UpdateExamIntegrationCommand(dominio.Id, results, true));

                }
                catch (Exception ex)
                {
                    await mediatorHandler.PublishLogEvent(new LogEvent(dominio.Id, $"ERRO na publicação da Mensagem para Domínio: {dominio.Id} - {ex.Message}"));
                }
            }
        }
    }
}
