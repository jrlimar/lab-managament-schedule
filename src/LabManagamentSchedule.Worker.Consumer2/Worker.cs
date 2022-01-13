using LabManagamentSchedule.Worker.Consumer2.ConsumersManager;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace LabManagamentSchedule.Worker.Consumer2
{
    public class Worker : BackgroundService
    {
        private readonly IConsumerManager consumerManager;

        public Worker(IConsumerManager consumerManager)
        {
            this.consumerManager = consumerManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await consumerManager.SetConsumers();
        }
    }
}
