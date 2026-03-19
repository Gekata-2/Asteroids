using System;
using System.Collections.Generic;

namespace _Project.Scripts.Services
{
    public class PauseService : IDisposable
    {
        public event Action GamePaused;
        public event Action GameResumed;
        
        private readonly List<IPausable> _items;

        public bool IsGamePaused { get; private set; }

        public PauseService(List<IPausable> startingItems = null)
        {
            _items = startingItems ?? new List<IPausable>();
        }

        public void AddItem(IPausable item)
        {
            if (!_items.Contains(item))
                _items.Add(item);
        }

        public void RemoveItem(IPausable item)
        {
            if (_items.Contains(item))
                _items.Remove(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public void PerformPause()
        {
            foreach (IPausable item in _items)
                item.Pause();

            IsGamePaused = true;
            GamePaused?.Invoke();
        }

        public void PerformResume()
        {
            foreach (IPausable item in _items)
                item.Resume();

            IsGamePaused = false;
            GameResumed?.Invoke();
        }

        public void Dispose()
        {
            Clear();
        }
    }
}