namespace SignalBus
{
    public interface ISignalPublisher
    {
        void Publish<TSignal>(TSignal signal);
    }
}