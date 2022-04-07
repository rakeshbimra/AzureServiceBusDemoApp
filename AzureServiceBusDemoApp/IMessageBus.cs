using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureServiceBusDemoApp
{
    public interface IMessageBus
    {
        Task PublishMessageAsync<T>(T message);
    }
}
