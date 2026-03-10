using Player;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace UI.Windows
{
    public class GameOverWindow : MonoBehaviour
    {
        [SerializeField] private GameObject window;

        private IInput _input;
        private PauseService _pauseService;
        private EventBus _eventBus;
        private UIManager _uiManager;
        private ExitGameService _exitGameService;

        [Inject]
        private void Construct(IInput input, PauseService pauseService, EventBus eventBus, UIManager uiManager,
            ExitGameService exitGameService)
        {
            _input = input;
            _pauseService = pauseService;
            _eventBus = eventBus;
            _uiManager = uiManager;
            _exitGameService = exitGameService;
        }

        private void Start()
        {
            _eventBus.Subscribe<PlayerDeadEvent>(OnPlayerDead);
            _input.UICancel += Input_OnUICancel;
            _input.UISubmit += Input_OnUISubmit;
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<PlayerDeadEvent>(OnPlayerDead);
            _input.UICancel -= Input_OnUICancel;
            _input.UISubmit -= Input_OnUISubmit;
        }

        private void Input_OnUISubmit()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void Input_OnUICancel()
        {
            _exitGameService.PerformExit();
        }

        private void OnPlayerDead(PlayerDeadEvent @event)
        {
            Open();
        }

        private void Open()
        {
            window.SetActive(true);
            _pauseService.PerformPause();
            _uiManager.SetState(UIState.GameOver);
        }
    }
}