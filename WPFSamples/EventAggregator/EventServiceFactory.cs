using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace PubSub
{
    public static class EventServiceFactory
    {
        private static Prism.Events.EventAggregator _eventSerice;
        private static readonly object _syncRoot = new object();

        public static EventAggregator EventService
        {
            get
            {
                lock (_syncRoot)
                {
                    if (_eventSerice == null)
                        _eventSerice = new Prism.Events.EventAggregator();
                    return _eventSerice;
                }
            }
        }
    }

    public class GenericEvent<TValue> : PubSubEvent<EventParameters<TValue>> { }

    public class GenericEvent : PubSubEvent<string> { }

    public class EventParameters<TValue>
    {
        public EventParameters(string topic, Action action = null, TValue value = default(TValue))
        {
            if (string.IsNullOrEmpty(topic))
                throw new ArgumentNullException(nameof(topic));
            Topic = topic;
            ExpectedAction = action;
            Value = value;
        }

        public string Topic { get; private set; }

        public Action ExpectedAction { get; private set; }

        public TValue Value { get; private set; }
    }
}
