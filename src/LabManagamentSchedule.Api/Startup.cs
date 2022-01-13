using Hangfire;
using Hangfire.MemoryStorage;
using LabManagamentSchedule.Core.AppSettings;
using LabManagamentSchedule.Core.Mediator;
using LabManagamentSchedule.Core.MessageBus;
using LabManagamentSchedule.Core.MessageBus.Queues;
using LabManagamentSchedule.Core.Messages.Notifications;
using LabManagamentSchedule.Domain.Commands;
using LabManagamentSchedule.Domain.Handlers;
using LabManagamentSchedule.Repositories.SqlServer;
using LabManagamentSchedule.Services.Gateways;
using LabManagamentSchedule.Services.Schedules;
using LabManagamentSchedule.Services.Security;
using ManagerExamsLabs.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;


namespace LabManagamentSchedule.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //AppSettings
            services.AddSingleton<KeySettings>(Configuration.GetSection("KeySettings").Get<KeySettings>());
            services.AddSingleton<AuthenticationBasicSettings>(Configuration.GetSection("AuthenticationBasic").Get<AuthenticationBasicSettings>());
            services.AddSingleton<RabbitSettings>(Configuration.GetSection("RabbitMq").Get<RabbitSettings>());
            services.AddSingleton<CkeckExamQueue>(Configuration.GetSection("Queus:CheckExams").Get<CkeckExamQueue>());

            //
            services.AddScoped<ICryptographyService, CryptographyService>();
            services.AddScoped<IResultSchedule, ResultSchedule>();
            services.AddScoped<IResultRepository, ResultRepository>();
        
            //Gateways
            services.AddHttpClient<IDominioGateway, DominioGateway>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(Configuration.GetValue<string>("Gateways:Dominio"));
            }).SetHandlerLifetime(TimeSpan.FromMinutes(5));

         
            //Hangfire
            services.AddHangfireServer();
            services.AddHangfire(config =>
                           config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                           .UseSimpleAssemblyNameTypeSerializer()
                           .UseDefaultTypeSerializer()
                           .UseMemoryStorage());

            //Communication
            services.AddMediatR(AppDomain.CurrentDomain.Load("LabManagamentSchedule.Domain"));
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IMessageBus, MessageBus>();

            //Handlers
            //services.AddScoped<IRequestHandler<UpdateExamIntegrationCommand, bool>, UpdateExamIntegratedHandler>();
            //services.AddScoped<INotificationHandler<Notification>, NotificationHandler>(); //tem que ser via api pelo escopo do request

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRecurringJobManager recurringJobManager, IServiceProvider serviceProvider, IBackgroundJobClient backgroundJobClient)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseHangfireDashboard("/dashboard");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            recurringJobManager.AddOrUpdate("Resultados", () => serviceProvider.GetService<IResultSchedule>().ExamsCheckedsFromDomain(), Cron.Hourly());
        }
    }
}
