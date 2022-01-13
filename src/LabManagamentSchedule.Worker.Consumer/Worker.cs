using LabManagamentSchedule.Worker.Consumer.ConsumerManager;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace LabManagamentSchedule.Worker.Consumer
{
    public class Worker : BackgroundService
    {
        private readonly IConsumerManagement management;

        public Worker(IConsumerManagement management)
        {
            this.management = management;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await management.Inicialize();
        }
    }
}
