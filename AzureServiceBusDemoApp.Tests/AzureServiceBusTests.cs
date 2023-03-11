using Azure.Messaging.ServiceBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Threading.Tasks;

[TestClass]
public class AzureServiceBusTests
{
    private ServiceBusSender _sender;

    [TestInitialize]
    public void Setup()
    {
        var connectionString = "your-connection-string";
        var queueName = "your-queue-name";
        _sender = new ServiceBusClient(connectionString).CreateSender(queueName);
    }

}
