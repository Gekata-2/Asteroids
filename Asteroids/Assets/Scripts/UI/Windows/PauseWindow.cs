using Player;
using Services;
using UnityEngine;
using Zenject;

namespace UI.Windows
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
            _input.PausePerformed += Input_OnPausePerformed;
            _input.UICancel += Input_OnUICancel;
        }

        private void OnDestroy()
        {
            _input.PausePerformed -= Input_OnPausePerformed;
            _input.UICancel -= Input_OnUICancel;
        }

        private void Input_OnUICancel()
        {
            if (_uiManager.CurrentState != UIState.Pause)
                return;

            _pauseService.PerformResume();
            content.SetActive(false);
            _uiManager.SetState(UIState.None);
        }

        private void Input_OnPausePerformed()
        {
            if (_pauseService.IsGamePaused || !_uiManager.CanOpenPause())
                return;

            _pauseService.PerformPause();
            content.SetActive(true);
            _uiManager.SetState(UIState.Pause);
        }
    }
}