using System;
using System.Collections.Generic;
using EventBus.Abstract;

namespace EventBus
{
    public class EventBus : IEventBus
    {
        private readonly IDictionary<Type, IListenerContainer> _containers;

        public EventBus() =>
            _containers = new Dictionary<Type, IListenerContainer>();

        public void Publish<TEvent>(TEvent args) =>
            Container<TEvent>(args.GetType()).Publish(args);

        public void Subscribe<TEvent>(IEventListener<TEvent> listener) =>
            Container<TEvent>().Subscribe(listener);

        public void Unsubscribe<TEvent>(IEventListener<TEvent> listener) =>
            Container<TEvent>().Unsubscribe(listener);
        
        private ListenerContainer<TEvent> Container<TEvent>() =>
            Container<TEvent>(typeof(TEvent));

        private ListenerContainer<TEvent> Container<TEvent>(Type type)
        {
            if (!_containers.TryGetValue(type, out var container))
                _containers.Add(type, container = new ListenerContainer<TEvent>());
            return container as ListenerContainer<TEvent>;
        }
        
        private interface IListenerContainer { }
        
        private class ListenerContainer<TListener> : IListenerContainer
        {
            private readonly ICollection<IEventListener<TListener>> _listeners;

            public ListenerContainer() =>
                _listeners = new HashSet<IEventListener<TListener>>();

            public void Publish(TListener args)
            {
                foreach (var listener in _listeners) 
                    listener.OnListen(args);
            }

            public void Subscribe(IEventListener<TListener> listener) =>
                _listeners.Add(listener);

            public void Unsubscribe(IEventListener<TListener> listener) =>
                _listeners.Add(listener);
        }
    }
}