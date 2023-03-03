using System.IO;

namespace SignalBus
{
    public class DebugSignalBus : ISignalBus
    {
        private readonly ISignalBus _signalBus;
        private readonly TextWriter _writer;

        public DebugSignalBus(ISignalBus signalBus, TextWriter writer)
        {
            _signalBus = signalBus;
            _writer = writer;
        }

        public void Publish<TSignal>(TSignal signal)
        {
            _writer.WriteLine($"Send {signal.GetType()}");
            _signalBus.Publish(signal);
        }

        public void Subscribe<TSignal>(ISignalListener<TSignal> listener)
        {
            _writer.WriteLine($"Subscribe {listener} to {typeof(TSignal)}");
            _signalBus.Subscribe(listener);
        }

        public void Unsubscribe<TSignal>(ISignalListener<TSignal> listener)
        {
            _writer.WriteLine($"Unsubscribe {listener} from {typeof(TSignal)}");
            _signalBus.Unsubscribe(listener);
        }
    }
}