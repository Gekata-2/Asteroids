using _Project.Scripts.Entities;
using _Project.Scripts.Entities.UFO;
using _Project.Scripts.GameOver;
using _Project.Scripts.Meta.Analytics;
using _Project.Scripts.Player;
using _Project.Scripts.Services;
using _Project.Scripts.Services.BeginGame;
using _Project.Scripts.Sfx;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Level
{
    public class LevelBootstrap : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private bool _isCursorVisible;

        private IInput _inputHandler;
        private EntitiesContainer _entitiesContainer;
        private PlayerStatePresenter _playerStatePresenter;
        private CursorService _cursorService;
        private GameOverModel _gameOverModel;
        private UfosSpawner _ufosSpawner;
        private IAnalyticsService _analyticsService;
        private BeginGameModel _beginGameModel;
        private AudioSystem _audioSystem;


        [Inject]
        private void Construct(IInput inputHandler,
            EntitiesContainer entitiesContainer,
            PlayerStatePresenter playerStatePresenter,
            CursorService cursorService,
            UfosSpawner ufosSpawner,
            GameOverModel gameOverModel,
            IAnalyticsService analyticsService,
            BeginGameModel beginGameModel,
            AudioSystem audioSystem)
        {
            _inputHandler = inputHandler;
            _entitiesContainer = entitiesContainer;
            _playerStatePresenter = playerStatePresenter;
            _cursorService = cursorService;
            _ufosSpawner = ufosSpawner;
            _gameOverModel = gameOverModel;
            _analyticsService = analyticsService;
            _beginGameModel = beginGameModel;
            _audioSystem = audioSystem;
        }

        private void Awake()
        {
            _cursorService.SetCursorVisibility(_isCursorVisible);
        }

        private void Start()
        {
            _inputHandler.Enable();
            _audioSystem.PlayMusic(SFX.LevelOst);
            BeginGame().Forget();
        }

        private async UniTask BeginGame()
        {
            await UniTask.WhenAll(_beginGameModel.ActivateConfigsData());
            
            _beginGameModel.FetchAssets();

            Player.Player player = _beginGameModel.SpawnPlayer(_playerSpawnPoint.position);
            _playerStatePresenter.SetPlayerModel(player.GetComponent<PlayerMovement>());
            _entitiesContainer.AddEntity(player.GetComponent<Entity>());
            _ufosSpawner.SetTarget(player.GetComponent<EnemyTarget>());
            _gameOverModel.SetPlayer(player);

            _beginGameModel.BeginGame();

            _analyticsService.LogGameStarted();
        }

        private void OnDestroy()
        {
            _inputHandler.Disable();
        }
    }
}