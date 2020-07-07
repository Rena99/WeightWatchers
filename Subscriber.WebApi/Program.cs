using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NServiceBus;
using System.Data.SqlClient;
using NServiceBus.Logging;
using Microsoft.Extensions.DependencyInjection;
using Subscriber.Services.Interfaces;
using AutoMapper;
using Subscriber.Services;

namespace Subscriber.WebApi
{

    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
         .SetBasePath(Directory.GetCurrentDirectory())
         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
         .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
         .Build();
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder()
           .UseNServiceBus(context =>
           {


               var endpointConfiguration = new EndpointConfiguration("WeightWatchers");

               endpointConfiguration.EnableInstallers();


               var outboxSettings = endpointConfiguration.EnableOutbox();

               outboxSettings.KeepDeduplicationDataFor(TimeSpan.FromDays(6));
               outboxSettings.RunDeduplicationDataCleanupEvery(TimeSpan.FromMinutes(15));
               var recoverability = endpointConfiguration.Recoverability();
               recoverability.Delayed(
                   customizations: delayed =>
                   {
                       delayed.NumberOfRetries(2);
                       delayed.TimeIncrease(TimeSpan.FromMinutes(4));
                   });

               recoverability.Immediate(
                   customizations: immediate =>
                   {
                       immediate.NumberOfRetries(3);

                   });

               var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
               transport.UseConventionalRoutingTopology()
                   .ConnectionString("host= localhost:5672; username = Rena; password = Rena@rabbitmq");


               var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
               var connection = "Server=.\\sqlexpress; Database= WeightWatchers; Trusted_Connection = True;";

               persistence.SqlDialect<SqlDialect.MsSqlServer>();

               persistence.ConnectionBuilder(
                   connectionBuilder: () =>
                   {
                       return new SqlConnection(connection);
                   });

               var subscriptions = persistence.SubscriptionSettings();
               subscriptions.CacheFor(TimeSpan.FromMinutes(10));
               endpointConfiguration.SendFailedMessagesTo("error");
               endpointConfiguration.AuditProcessedMessagesTo("audit");
               //endpointConfiguration.AuditSagaStateChanges(
               //        serviceControlQueue: "Particular.weightwatchers");
               var conventions = endpointConfiguration.Conventions();
               conventions.DefiningCommandsAs(type => type.Namespace == "Messages.Commands");
               conventions.DefiningEventsAs(type => type.Namespace == "Messages.Events");
               SubscribeToNotifications.Subscribe(endpointConfiguration);

               return endpointConfiguration;
           })
           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.UseStartup<Startup>()
                         .UseConfiguration(Configuration);
           });
    }
}
 