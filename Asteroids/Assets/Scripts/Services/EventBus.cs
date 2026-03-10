using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Services
{
    public class PlayerDeadEvent
    {
        public readonly object Killer;

        public PlayerDeadEvent(object killer)
        {
            Killer = killer;
        }
    }
    
    public class EventBus
    {
        private Dictionary<string, List<object>> _callbacks = new();

        public void Subscribe<T>(Action<T> callback)
        {
            string key = GetKey<T>();
            
            if (_callbacks.ContainsKey(key))
                _callbacks[key].Add(callback);
            else
                _callbacks.Add(key, new List<object> { callback });
        }

        public void Unsubscribe<T>(Action<T> callback)
        {
            string key = GetKey<T>();
            if (_callbacks.ContainsKey(key))
                _callbacks[key].Remove(callback);
            else
                Debug.LogError("Unsubscribing non existing callback");
        }

        public void Invoke<T>(T @event)
        {
            string key = GetKey<T>();
            if (_callbacks.TryGetValue(key, out List<object> callbacks))
            {
                foreach (Action<T> action in callbacks.Select(callback => callback as Action<T>))
                    action?.Invoke(@event);
            }
        }

        private string GetKey<T>() 
            => typeof(T).Name;
    }
}