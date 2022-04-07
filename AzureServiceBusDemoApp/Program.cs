using AzureServiceBusDemoApp;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddTransient<App>();
services.AddSingleton<IMessageBusFactory, AzureServiceBusFactory>();
var serviceProvider = services.BuildServiceProvider();
await serviceProvider.GetService<App>().Run();
