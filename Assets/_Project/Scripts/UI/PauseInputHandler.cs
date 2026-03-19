using System;
using _Project.Scripts.Player;
using _Project.Scripts.Services;
using Zenject;

namespace _Project.Scripts.UI
{
    public class PauseInputHandler : IInitializable, IDisposable
    {
        private readonly IInput _input;
        private readonly PauseService _pauseService;

        public PauseInputHandler(IInput input, PauseService pauseService)
        {
            _input = input;
            _pauseService = pauseService;
        }

        public void Initialize()
        {
            _input.PausePerformed += OnPausePerformed;
            _input.UICancelPerformed += OnUICancelPerformed;
        }

        private void OnPausePerformed()
        {
            if (_pauseService.IsGamePaused)
                return;
            
            _pauseService.PerformPause();
        }

        private void OnUICancelPerformed()
        {
            if (!_pauseService.IsGamePaused)
                return;

            _pauseService.PerformResume();
        }

        public void Dispose()
        {
            _input.PausePerformed -= OnPausePerformed;
            _input.UICancelPerformed -= OnUICancelPerformed;
        }
    }
}