using System;
using _Project.Scripts.Player;
using Zenject;

namespace _Project.Scripts.UI
{
    public class WindowsInputHandler : IInitializable, IDisposable
    {
        private readonly IInput _input;
        private readonly UIManager _uiManager;

        public WindowsInputHandler(IInput input, UIManager uiManager)
        {
            _input = input;
            _uiManager = uiManager;
        }

        public void Initialize()
        {
            _input.PausePerformed += OnPausePerformed;
            _input.UICancelPerformed += OnUICancelPerformed;
        }

        private void OnUICancelPerformed()
        {
            _uiManager.PerformCancel();
        }

        private void OnPausePerformed()
        {
            _uiManager.OpenPause();
        }

        public void Dispose()
        {
            _input.PausePerformed -= OnPausePerformed;
            _input.UICancelPerformed -= OnUICancelPerformed;
        }
    }
}