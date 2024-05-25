using Build_IT_NCalcPlayground.Events.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_NCalcPlayground.Events
{
    public class EventAggregator : IEventAggregator
    {
        private readonly Dictionary<Type, object> _events = new();

        public T GetEvent<T>() where T : BaseEvent, new()
        {
            var type = typeof(T);
            if (_events.TryGetValue(type, out object @event))
                return (T)@event;
            var newEvent = Activator.CreateInstance(type);
            _events.Add(type, newEvent);
            return (T)newEvent;
        }
    }
}
