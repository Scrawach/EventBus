using System.IO;
using EventBus.Abstract;

namespace EventBus
{
    public class DebugEventBus : IEventBus
    {
        private readonly IEventBus _eventBus;
        private readonly TextWriter _writer;

        public DebugEventBus(IEventBus eventBus, TextWriter writer)
        {
            _eventBus = eventBus;
            _writer = writer;
        }

        public void Publish<TEvent>(TEvent args)
        {
            _writer.WriteLine($"Send {args.GetType()}");
            _eventBus.Publish(args);
        }

        public void Subscribe<TEvent>(IEventListener<TEvent> listener)
        {
            _writer.WriteLine($"Subscribe {listener} to {typeof(TEvent)}");
            _eventBus.Subscribe(listener);
        }

        public void Unsubscribe<TEvent>(IEventListener<TEvent> listener)
        {
            _writer.WriteLine($"Unsubscribe {listener} from {typeof(TEvent)}");
            _eventBus.Unsubscribe(listener);
        }
    }
}