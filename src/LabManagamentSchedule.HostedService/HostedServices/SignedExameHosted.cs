using LabManagamentSchedule.HostedService.Consumers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LabManagamentSchedule.HostedService
{
    public class SignedExameHosted : IHostedService
    {
        private readonly ISignedExamConsumer consumer;
        private readonly ILogger<SignedExameHosted> _logger;

        public SignedExameHosted(ILogger<SignedExameHosted> logger, ISignedExamConsumer consumer)
        {
            _logger = logger;
            this.consumer = consumer;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"Iniciando Fila {nameof(ISignedExamConsumer)} - {DateTime.UtcNow}");

            await consumer.Receive();

            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Finalizado Fila 1");

            await Task.CompletedTask;
        }
    }
}
