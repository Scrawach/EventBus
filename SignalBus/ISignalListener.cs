namespace SignalBus
{
    public interface ISignalListener<in TSignal>
    {
        void OnListen(TSignal signal);
    }
}