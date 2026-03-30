public interface IServiceBus
{
    void Publish<T>(T message);
}