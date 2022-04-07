using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureServiceBusDemoApp
{
    public class App
    {
        private readonly IMessageBusFactory _messageBusFactory;

        public App(IMessageBusFactory messageBusFactory)
        {
            this._messageBusFactory = messageBusFactory;
        }

        public async Task Run()
        {
            var client = this._messageBusFactory.GetClient("TOPIC_NAME");

            await client.PublishMessageAsync(new
            {
                FirstName = "FIRSTNAME",
                LastName = "LASTNAME"
            });
        }
    }
}
