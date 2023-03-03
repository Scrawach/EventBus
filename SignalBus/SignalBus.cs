using System;
using System.Collections.Generic;

namespace SignalBus
{
    public class SignalBus : ISignalBus
    {
        private readonly IDictionary<Type, ISignalBus> _containers;

        public SignalBus() =>
            _containers = new Dictionary<Type, ISignalBus>();

        public void Publish<TSignal>(TSignal signal) =>
            Container<TSignal>(signal.GetType()).Publish(signal);

        public void Subscribe<TSignal>(ISignalListener<TSignal> listener) =>
            Container<TSignal>().Subscribe(listener);

        public void Unsubscribe<TSignal>(ISignalListener<TSignal> listener) =>
            Container<TSignal>().Unsubscribe(listener);
        
        private ISignalBus Container<TSignal>() =>
            Container<TSignal>(typeof(TSignal));

        private ISignalBus Container<TSignal>(Type type)
        {
            if (!_containers.TryGetValue(type, out var container))
                _containers.Add(type, container = new ListenerContainer<TSignal>());
            return container;
        }
        
        private class ListenerContainer<TListener> : ISignalBus
        {
            private readonly ICollection<ISignalListener<TListener>> _listeners;

            public ListenerContainer() =>
                _listeners = new HashSet<ISignalListener<TListener>>();

            public void Publish<TSignal>(TSignal signal)
            {
                var castedSignal = (TListener) (signal as object); 
                
                foreach (var listener in _listeners) 
                    listener.OnListen(castedSignal);
            }

            public void Subscribe<TSignal>(ISignalListener<TSignal> listener) =>
                _listeners.Add((ISignalListener<TListener>) listener);

            public void Unsubscribe<TSignal>(ISignalListener<TSignal> listener) =>
                _listeners.Add((ISignalListener<TListener>) listener);
        }
    }
}