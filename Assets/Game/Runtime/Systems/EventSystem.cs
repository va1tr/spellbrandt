using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spellbrandt
{
    public sealed class EventSystem : MonoBehaviour
    {
        private static readonly Dictionary<Type, List<object>> _subscriptions = new Dictionary<Type, List<object>>();

        public static void Subscribe<T>(Action<T> handler)
        {
            var key = typeof(T);

            if (!_subscriptions.ContainsKey(key))
            {
                _subscriptions.Add(key, new List<object>());
            }

            var listeners = _subscriptions[key];

            if (!listeners.Contains(handler))
            {
                listeners.Add(handler);
            }
        }

        public static void Publish<T>(T args)
        {
            if (_subscriptions.TryGetValue(typeof(T), out var listeners))
            {
                for (int i = listeners.Count -1; i >= 0; i--)
                {
                    ((Action<T>)listeners[i])?.Invoke(args);
                }
            }
        }

        public static void Unsubscribe<T>(Action<T> handler)
        {
            var key = typeof(T);

            if (_subscriptions.ContainsKey(key))
            {
                var listeners = _subscriptions[key];

                if (listeners.Contains(handler))
                {
                    listeners.Remove(handler);
                }
            }
        }
    }
}
