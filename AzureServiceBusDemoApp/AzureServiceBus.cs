using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureServiceBusDemoApp
{
    public class AzureServiceBus : IMessageBus
    {
        private readonly ServiceBusSender _serviceBusSender;

        public AzureServiceBus(ServiceBusSender serviceBusSender)
        {
            this._serviceBusSender = serviceBusSender;
        }

        public async Task PublishMessageAsync<T>(T message)
        {
            var jsonString = JsonSerializer.Serialize(message);

            var serviceBusMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonString));

            await this._serviceBusSender.SendMessageAsync(serviceBusMessage);
        }

        internal static IMessageBus Create(ServiceBusSender sender)
        {
            return new AzureServiceBus(sender);
        }
    }
}
