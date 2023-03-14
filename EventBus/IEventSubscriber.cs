namespace EventBus
{
    public interface IEventSubscriber
    {
        void Subscribe<TEvent>(IEventListener<TEvent> listener);
        void Unsubscribe<TEvent>(IEventListener<TEvent> listener);
    }
}