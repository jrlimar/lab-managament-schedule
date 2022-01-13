using LabManagamentSchedule.Worker.Consumer.ConsumerManager;
using LabManagamentSchedule.Worker.Consumer.Consumers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LabManagamentSchedule.Worker.Consumer
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
                    services.AddHostedService<Worker>();                  
                    services.AddSingleton<IConsumerManagement, ConsumerManagement>();
                    services.AddSingleton<IConsumerCheckExam, ConsumerCheckExam>();
                    services.AddSingleton<IConsumerTeste, ConsumerTeste>();
                });
    }
}
