using Microsoft.Extensions.Configuration;
using Notifications;
using NServiceBus;
using NServiceBus.Logging;
using RabbitMQ.Client;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace NewIsolation
{
    class Program
    {

        static async Task Main()
        {
            Console.Title = "MeasureService";
            var endpointConfiguration = new EndpointConfiguration("MeasureService");
            //endpointConfiguration.AuditProcessedMessagesTo("audit");
            //endpointConfiguration.AuditSagaStateChanges(serviceControlQueue: "particular.coronaproject");
            var defaultFactory = LogManager.Use<DefaultFactory>();
            defaultFactory.Level(LogLevel.Info);
            var recoverability = endpointConfiguration.Recoverability();
            recoverability.Delayed(
                customizations: delayed =>
                {
                    delayed.TimeIncrease(TimeSpan.FromSeconds(1));
                });
            SubscribeToNotifications.Subscribe(endpointConfiguration);
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            var connectionR = "host= localhost:5672; username = Rena; password = Rena@rabbitmq;";
            var connectionS = "Server=.\\sqlexpress; Database= WeightWatchers; Trusted_Connection = True;";
            transport.UseConventionalRoutingTopology();
            transport.ConnectionString(connectionR);
            endpointConfiguration.EnableOutbox();
            endpointConfiguration.EnableInstallers();
            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            var subscriptions = persistence.SubscriptionSettings();
            subscriptions.CacheFor(TimeSpan.FromMinutes(1));
            persistence.SqlDialect<SqlDialect.MsSqlServer>();
            persistence.ConnectionBuilder(
                connectionBuilder: () =>
                {
                    return new SqlConnection(connectionS);
                });
            var routing = transport.Routing();
            var scanner = endpointConfiguration.AssemblyScanner();
            scanner.ExcludeAssemblies("System.Configuratuion.ConfigurationManager");
            scanner.ThrowExceptions = false;
            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
