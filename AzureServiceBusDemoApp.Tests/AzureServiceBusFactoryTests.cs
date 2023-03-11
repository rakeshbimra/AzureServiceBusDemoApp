using AzureServiceBusDemoApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class AzureServiceBusFactoryTests
{
    private const string ServiceBusConnectionString = "ServiceBusConnectionString";

    private AzureServiceBusFactory _azureServiceBusFactory;

    [TestInitialize]
    public void Setup()
    {
        Environment.SetEnvironmentVariable(ServiceBusConnectionString, "your-connection-string");
        _azureServiceBusFactory = new AzureServiceBusFactory();
    }

    [TestMethod]
    public void GetClient_Returns_Instance_Of_AzureServiceBus()
    {
        // Arrange
        var senderName = "your-sender-name";

        // Act
        var result = _azureServiceBusFactory.GetClient(senderName);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(AzureServiceBus));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void GetClient_Throws_ArgumentNullException_If_Connection_String_Is_Not_Configured()
    {
        // Arrange
        Environment.SetEnvironmentVariable(ServiceBusConnectionString, null);
        var senderName = "your-sender-name";

        // Act
        var result = _azureServiceBusFactory.GetClient(senderName);

        // Assert
        // The test should throw an ArgumentNullException because the connection string is not configured
    }
}
