namespace EventBus.Abstract
{
    public interface IEventListener<in TEvent>
    {
        void OnListen(TEvent args);
    }
}