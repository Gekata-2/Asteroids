using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Services.EventBus
{
    public class EventBus
    {
        private readonly Dictionary<string, List<object>> _callbacks = new();

        public void Subscribe<T>(Action<T> callback)
        {
            string key = GetKey<T>();
            
            if (_callbacks.TryGetValue(key, out List<object> callbacks))
                callbacks.Add(callback);
            else
                _callbacks.Add(key, new List<object> { callback });
        }

        public void Unsubscribe<T>(Action<T> callback)
        {
            string key = GetKey<T>();
            if (_callbacks.TryGetValue(key, out List<object> callbacks))
                callbacks.Remove(callback);
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