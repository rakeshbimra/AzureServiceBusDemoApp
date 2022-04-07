using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureServiceBusDemoApp
{
    public class AzureServiceBusFactory : IMessageBusFactory
    {
        private const string ServiceBusConnectionString = "ServiceBusConnectionString";

        private readonly object _lockObject = new object();

        private readonly ConcurrentDictionary<string, ServiceBusClient> _clients = new ConcurrentDictionary<string, ServiceBusClient>();

        private readonly ConcurrentDictionary<string, ServiceBusSender> _senders = new ConcurrentDictionary<string, ServiceBusSender>();

        public IMessageBus GetClient(string senderName)
        {
            var connectionString = Environment.GetEnvironmentVariable(ServiceBusConnectionString);

            Ensure.ConditionIsMet(!string.IsNullOrEmpty(connectionString),
                () => throw new ArgumentNullException($"{nameof(connectionString)} not configured"));

            var key = $"{connectionString}-{senderName}";

            if (this._senders.ContainsKey(key) && !this._senders[key].IsClosed)
            {
                return AzureServiceBus.Create(this._senders[key]);
            }

            var client = this.GetServiceBusClient(connectionString);

            lock (this._lockObject)
            {
                if (this._senders.ContainsKey(key) && this._senders[key].IsClosed)
                {
                    return AzureServiceBus.Create(this._senders[key]);
                }

                var sender = client.CreateSender(senderName);

                this._senders[key] = sender;
            }

            return AzureServiceBus.Create(this._senders[key]);
        }


        protected virtual ServiceBusClient GetServiceBusClient(string connectionString)
        {
            var key = $"{connectionString}";

            lock (this._lockObject)
            {
                if (this.ClientDoesntExistOrIsClosed(connectionString))
                {
                    var client = new ServiceBusClient(connectionString, new ServiceBusClientOptions
                    {
                        TransportType = ServiceBusTransportType.AmqpTcp
                    });

                    this._clients[key] = client;
                }

                return this._clients[key];
            }
        }

        private bool ClientDoesntExistOrIsClosed(string connectionString)
        {
            return !this._clients.ContainsKey(connectionString) || this._clients[connectionString].IsClosed;
        }
    }
}
