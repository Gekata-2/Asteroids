using _Project.Scripts.Player;
using _Project.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI.Windows
{
    public class PauseWindow : MonoBehaviour
    {
        [SerializeField] private GameObject content;

        private IInput _input;
        private PauseService _pauseService;
        private UIManager _uiManager;

        [Inject]
        private void Construct(IInput input, PauseService pauseService, UIManager uiManager)
        {
            _input = input;
            _pauseService = pauseService;
            _uiManager = uiManager;
        }

        private void Start()
        {
            _input.PausePerformed += OnPausePerformed;
            _input.UICancelPerformed += OnUICancelPerformed;
        }

        private void OnDestroy()
        {
            _input.PausePerformed -= OnPausePerformed;
            _input.UICancelPerformed -= OnUICancelPerformed;
        }

        private void OnUICancelPerformed()
        {
            if (_uiManager.CurrentState != UIState.Pause)
                return;

            _pauseService.PerformResume();
            content.SetActive(false);
            _uiManager.SetState(UIState.None);
        }

        private void OnPausePerformed()
        {
            if (_pauseService.IsGamePaused || !_uiManager.CanOpenPause())
                return;

            _pauseService.PerformPause();
            content.SetActive(true);
            _uiManager.SetState(UIState.Pause);
        }
    }
}