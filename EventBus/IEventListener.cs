namespace EventBus
{
    public interface IEventListener<in TEvent>
    {
        void OnListen(TEvent args);
    }
}