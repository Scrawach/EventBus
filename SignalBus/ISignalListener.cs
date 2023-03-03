namespace SignalBus
{
    public interface ISignalListener<in TSignal>
    {
        void Listen(TSignal signal);
    }
}