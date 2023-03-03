namespace SignalBus
{
    public interface ISignalBus
    {
        void Publish<TSignal>(TSignal signal);
        void Subscribe<TSignal>(ISignalListener<TSignal> listener);
        void Unsubscribe<TSignal>(ISignalListener<TSignal> listener);
    }
}