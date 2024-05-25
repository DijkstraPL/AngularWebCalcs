using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_NCalcPlayground.Events
{
    public abstract class AppEvent : BaseEvent
    {
        private readonly List<Action> _subscribers = new List<Action>();

        public void Subscribe(Action action)
        {
            if (action is null)
                throw new ArgumentNullException(nameof(action));

            _subscribers.Add(action);
        }

        public void Publish()
        {
            _subscribers.ForEach(s => s());
        }
    }
    public abstract class AppEvent<T> : BaseEvent
    {
        private readonly List<Action<T>> _subscribers = new List<Action<T>>();

        public void Subscribe(Action<T> action)
        {
            if (action is null)
                throw new ArgumentNullException(nameof(action));

            _subscribers.Add(action);
        }

        public void Publish(T data)
        {
            _subscribers.ForEach(s => s(data));
        }
    }

    public abstract class BaseEvent { }
}
