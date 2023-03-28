using System;
using System.Collections.Generic;
using EventBus.Abstract;

namespace EventBus
{
    public class EventBus : IEventBus
    {
        private readonly IDictionary<Type, IEventBus> _containers;

        public EventBus() =>
            _containers = new Dictionary<Type, IEventBus>();

        public void Publish<TEvent>(TEvent args) =>
            Container<TEvent>(args.GetType()).Publish(args);

        public void Subscribe<TEvent>(IEventListener<TEvent> listener) =>
            Container<TEvent>().Subscribe(listener);

        public void Unsubscribe<TEvent>(IEventListener<TEvent> listener) =>
            Container<TEvent>().Unsubscribe(listener);
        
        private IEventBus Container<TEvent>() =>
            Container<TEvent>(typeof(TEvent));

        private IEventBus Container<TEvent>(Type type)
        {
            if (!_containers.TryGetValue(type, out var container))
                _containers.Add(type, container = new ListenerContainer<TEvent>());
            return container;
        }
        
        private class ListenerContainer<TListener> : IEventBus
        {
            private readonly ICollection<IEventListener<TListener>> _listeners;

            public ListenerContainer() =>
                _listeners = new HashSet<IEventListener<TListener>>();

            public void Publish<TEvent>(TEvent args)
            {
                var castedSignal = (TListener) (args as object);
                foreach (var listener in _listeners) 
                    listener.OnListen(castedSignal);
            }

            public void Subscribe<TEvent>(IEventListener<TEvent> listener) =>
                _listeners.Add((IEventListener<TListener>) listener);

            public void Unsubscribe<TEvent>(IEventListener<TEvent> listener) =>
                _listeners.Remove((IEventListener<TListener>) listener);
        }
    }
}