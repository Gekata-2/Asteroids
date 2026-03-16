using Player;
using Services;
using Services.EventBus;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace UI.Windows
{
    public class GameOverWindow : MonoBehaviour
    {
        [SerializeField] private GameObject window;
        [SerializeField] private TMP_Text score;

        private IInput _input;
        private PauseService _pauseService;
        private EventBus _eventBus;
        private UIManager _uiManager;
        private ExitGameService _exitGameService;
        private PlayerModel _playerModel;

        [Inject]
        private void Construct(IInput input, PauseService pauseService, EventBus eventBus, UIManager uiManager,
            ExitGameService exitGameService, PlayerModel playerModel)
        {
            _input = input;
            _pauseService = pauseService;
            _eventBus = eventBus;
            _uiManager = uiManager;
            _exitGameService = exitGameService;
            _playerModel = playerModel;
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
            if (_uiManager.CurrentState != UIState.GameOver)
                return;

            _exitGameService.PerformExit();
        }

        private void OnPlayerDead(PlayerDeadEvent @event)
        {
            Open();
        }

        private void Open()
        {
            score.text = $"{_playerModel.Score}";
            window.SetActive(true);
            _pauseService.PerformPause();
            _uiManager.SetState(UIState.GameOver);
        }
    }
}