using Player;
using Services;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class PauseWindow : MonoBehaviour
    {
        [SerializeField] private GameObject content;

        private IInput _input;
        private PauseService _pauseService;

        [Inject]
        private void Construct(IInput input, PauseService pauseService)
        {
            _input = input;
            _pauseService = pauseService;
        }

        private void Start()
        {
            _input.PausePerformed += Input_OnPausePerformed;
            content.SetActive(false);
        }

        private void OnDestroy()
        {
            _input.PausePerformed -= Input_OnPausePerformed;
        }

        private void Input_OnPausePerformed()
        {
            if (_pauseService.IsGamePaused)
                _pauseService.PerformResume();
            else
                _pauseService.PerformPause();

            content.SetActive(_pauseService.IsGamePaused);
        }
    }
}