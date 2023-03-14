namespace EventBus
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent args);
    }
}