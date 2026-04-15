using _Project.Scripts.Analytics;
using _Project.Scripts.Entities;
using _Project.Scripts.Entities.UFO;
using _Project.Scripts.Level.GameSession;
using _Project.Scripts.Player;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services
{
    public class LevelBootstrap : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private bool _isCursorVisible;

        private PlayerFactory _playerFactory;
        private IInput _inputHandler;
        private EntitiesContainer _entitiesContainer;
        private PlayerStatePresenter _playerStatePresenter;
        private CursorService _cursorService;
        private GameOverModel _gameOverModel;
        private UfosSpawner _ufosSpawner;
        private IAnalytics _analytics;

        [Inject]
        private void Construct(PlayerFactory playerFactory,
            IInput inputHandler,
            EntitiesContainer entitiesContainer,
            PlayerStatePresenter playerStatePresenter,
            CursorService cursorService,
            UfosSpawner ufosSpawner,
            GameOverModel gameOverModel,
            IAnalytics analytics)
        {
            _playerFactory = playerFactory;
            _inputHandler = inputHandler;
            _entitiesContainer = entitiesContainer;
            _playerStatePresenter = playerStatePresenter;
            _cursorService = cursorService;
            _ufosSpawner = ufosSpawner;
            _gameOverModel = gameOverModel;
            _analytics = analytics;
        }

        private void Awake()
        {
            Player.Player player = _playerFactory.Create();

            player.transform.position = _playerSpawnPoint.position;
            _playerStatePresenter.SetPlayerModel(player.GetComponent<PlayerMovement>());
            _entitiesContainer.AddEntity(player.GetComponent<Entity>());

            _ufosSpawner.SetTarget(player.GetComponent<EnemyTarget>());
            _gameOverModel.SetPlayer(player);
        }

        private void Start()
        {
            _cursorService.SetCursorVisibility(_isCursorVisible);
            _inputHandler.Enable();
            _analytics.LogGameStarted();
        }

        private void OnDestroy()
        {
            _inputHandler.Disable();
        }
    }
}