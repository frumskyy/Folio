using System.Text.Json;
using Azure.Messaging.ServiceBus;

namespace Folio.Infrastructure.ExternalServices;

public class AzureServiceBusService : IMessageQueueService, IAsyncDisposable
{
    private readonly ServiceBusSender _sender;

    public AzureServiceBusService(ServiceBusClient client, string queueName)
    {
        _sender = client.CreateSender(queueName);
    }

    public async Task PublishAsync<T>(T message)
    {
        var json = JsonSerializer.Serialize(message);
        var serviceBusMessage = new ServiceBusMessage(json)
        {
            ContentType = "application/json"
        };

        await _sender.SendMessageAsync(serviceBusMessage);
    }

    public async ValueTask DisposeAsync()
    {
        await _sender.DisposeAsync();
    }
}
