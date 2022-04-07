using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureServiceBusDemoApp
{
    public interface IMessageBusFactory
    {
        IMessageBus GetClient(string sender);
    }
}
