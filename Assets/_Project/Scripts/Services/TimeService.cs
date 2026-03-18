using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services
{
    public class TimeService : ITickable
    {
        public float TimeElapsed { get; private set; }
        private readonly PauseService _pauseService;

        public TimeService(PauseService pauseService)
        {
            _pauseService = pauseService;
        }

        public void Tick()
        {
            if (_pauseService.IsGamePaused)
                return;

            TimeElapsed += Time.deltaTime;
        }
    }
}