// Interfaces/IMessageQueueService.cs
public interface IMessageQueueService
{
    Task PublishAsync<T>(T message);
}