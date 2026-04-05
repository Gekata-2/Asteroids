using _Project.Scripts.Entities;
using _Project.Scripts.Entities.Factories;
using _Project.Scripts.Entities.UFO;
using _Project.Scripts.Level.GameSession;
using _Project.Scripts.Player;
using _Project.Scripts.Services.Pause;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services
{
    public class LevelBootstrap : MonoBehaviour
    {
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private bool isCursorVisible;

        private PlayerFactory _playerFactory;
        private IInput _inputHandler;
        private EntitiesContainer _entitiesContainer;
        private PlayerStatePresenter _playerStatePresenter;
        private CursorService _cursorService;
        private GameOverModel _gameOverModel;
        private UfosSpawner _ufosSpawner;

        [Inject]
        private void Construct(PlayerFactory playerFactory,
            IInput inputHandler,
            EntitiesContainer entitiesContainer,
            PlayerStatePresenter playerStatePresenter,
            CursorService cursorService,
            UfosSpawner ufosSpawner,
            GameOverModel gameOverModel,
            PauseService pauseService = null)
        {
            _playerFactory = playerFactory;
            _inputHandler = inputHandler;
            _entitiesContainer = entitiesContainer;
            _playerStatePresenter = playerStatePresenter;
            _cursorService = cursorService;

            _ufosSpawner = ufosSpawner;
            _gameOverModel = gameOverModel;
        }

        private void Awake()
        {
            Player.Player player = _playerFactory.Create();

            player.transform.position = playerSpawnPoint.position;
            _playerStatePresenter.SetPlayerModel(player.GetComponent<PlayerMovement>());
            _entitiesContainer.AddEntity(player.GetComponent<Entity>());

            _ufosSpawner.SetTarget(player.GetComponent<EnemyTarget>());
            _gameOverModel.SetPlayer(player);
        }

        private void Start()
        {
            _cursorService.SetCursorVisibility(isCursorVisible);
            _inputHandler.Enable();
        }

        private void OnDestroy()
        {
            _inputHandler.Disable();
        }
    }
}