namespace SignalBus
{
    public interface ISignalSubscriber
    {
        void Subscribe<TSignal>(ISignalListener<TSignal> listener);
        void Unsubscribe<TSignal>(ISignalListener<TSignal> listener);
    }
}