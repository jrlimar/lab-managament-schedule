using LabManagamentSchedule.Worker.Consumer2.Consumers;
using LabManagamentSchedule.Worker.Consumer2.Consumers.Interfaces;
using LabManagamentSchedule.Worker.Consumer2.ConsumersManager;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LabManagamentSchedule.Worker.Consumer2
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
                    services.AddSingleton<IManager, Manager>();
                    services.AddSingleton<IConsumerManager, ConsumerManager>();
                    services.AddSingleton<ICheckExamConsumer, CheckExamConsumer>();
                    services.AddSingleton<ITesteConsumer, TesteConsumer>();
                });
    }
}
