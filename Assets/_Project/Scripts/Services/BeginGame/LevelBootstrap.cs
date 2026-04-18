using _Project.Scripts.Analytics;
using _Project.Scripts.Entities;
using _Project.Scripts.Entities.UFO;
using _Project.Scripts.Level.GameSession;
using _Project.Scripts.Player;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services.BeginGame
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
        private IAnalytics _analytics;
        private BeginGameModel _beginGameModel;
        private LevelAssetsConfig _levelAssetsConfig;

        [Inject]
        private void Construct(IInput inputHandler,
            EntitiesContainer entitiesContainer,
            PlayerStatePresenter playerStatePresenter,
            CursorService cursorService,
            UfosSpawner ufosSpawner,
            GameOverModel gameOverModel,
            IAnalytics analytics,
            BeginGameModel beginGameModel,
            LevelAssetsConfig levelAssetsConfig)
        {
            _inputHandler = inputHandler;
            _entitiesContainer = entitiesContainer;
            _playerStatePresenter = playerStatePresenter;
            _cursorService = cursorService;
            _ufosSpawner = ufosSpawner;
            _gameOverModel = gameOverModel;
            _analytics = analytics;
            _beginGameModel = beginGameModel;
            _levelAssetsConfig = levelAssetsConfig;
        }

        private void Awake()
        {
            _cursorService.SetCursorVisibility(_isCursorVisible);
        }

        private void Start()
        {
            _inputHandler.Enable();
            BeginGame().Forget();
        }

        private async UniTask BeginGame()
        {
            await _beginGameModel.PreloadAssets(_levelAssetsConfig.UsedAssets);
            _beginGameModel.FetchAssets();

            Player.Player player = _beginGameModel.SpawnPlayer(_playerSpawnPoint.position);
            _playerStatePresenter.SetPlayerModel(player.GetComponent<PlayerMovement>());
            _entitiesContainer.AddEntity(player.GetComponent<Entity>());
            _ufosSpawner.SetTarget(player.GetComponent<EnemyTarget>());
            _gameOverModel.SetPlayer(player);

            _beginGameModel.BeginGame();

            _analytics.LogGameStarted();
        }

        private void OnDestroy()
        {
            _inputHandler.Disable();
        }
    }
}