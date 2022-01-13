using LabManagamentSchedule.HostedService.Consumers;
using LabManagamentSchedule.HostedService.Manager;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LabManagamentSchedule.HostedService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<SignedExameHosted>();
                    services.AddHostedService<TesteHosted>();

                    services.AddSingleton<IQueueManager, QueueManager>();
                    services.AddSingleton<ISignedExamConsumer, SignedExamConsumer>();
                    services.AddSingleton<ITesteExamConsumer, TesteExamConsumer>();
                });
    }
}
