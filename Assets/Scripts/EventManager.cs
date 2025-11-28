using System;
using System.Collections.Generic;

namespace Root {
    public static class EventManager 
    {
        private static readonly Dictionary<Type, Delegate> EventDictionary = new();
    
        public static void Subscribe<T>(Action<T> listener) {
            Type eventType = typeof(T);
            if (EventDictionary.TryGetValue(eventType, out var existing))
                EventDictionary[eventType] = Delegate.Combine(existing, listener);
            else
                EventDictionary[eventType] = listener;
        }
    
        public static void Unsubscribe<T>(Action<T> listener) {
            Type eventType = typeof(T);
            if (!EventDictionary.TryGetValue(eventType, out var existing)) return;
            var newDelegate = Delegate.Remove(existing, listener);
            if (newDelegate == null)
                EventDictionary.Remove(eventType);
            else
                EventDictionary[eventType] = newDelegate;
        }
    
        public static void Trigger<T>(T eventData) {
            Type eventType = typeof(T);
            if (!EventDictionary.TryGetValue(eventType, out var del)) return;

            if (del is Action<T> action)
                action.Invoke(eventData);
        }
    }
}