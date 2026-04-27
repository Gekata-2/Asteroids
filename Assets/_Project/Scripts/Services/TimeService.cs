using _Project.Scripts.Services.BeginGame;
using _Project.Scripts.Services.Pause;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services
{
    public class TimeService : ITickable, IGameStarter, IPausable
    {
        private bool _isPaused;
        private bool _isEnabled;
        
        public float TimeElapsed { get; private set; }

        public void Tick()
        {
            if (!_isPaused && _isEnabled)
                TimeElapsed += Time.deltaTime;
        }

        public void BeginGame()
            => _isEnabled = true;

        public void Pause()
            => _isPaused = false;

        public void Resume()
            => _isPaused = true;
    }
}