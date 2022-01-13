using LabManagamentSchedule.HostedService.Consumers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LabManagamentSchedule.HostedService
{
    public class TesteHosted : IHostedService
    {
        private readonly ITesteExamConsumer consumer;
        private readonly ILogger<TesteHosted> _logger;
        public TesteHosted(ILogger<TesteHosted> logger, ITesteExamConsumer consumer)
        {
            _logger = logger;
            this.consumer = consumer;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"Iniciando Fila {nameof(ITesteExamConsumer)} - {DateTime.UtcNow}");

            await consumer.Receive();

            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Finalizado Fila 2");

            await Task.CompletedTask;
        }
    }
}
