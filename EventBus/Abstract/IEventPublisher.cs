namespace EventBus.Abstract
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent args);
    }
}