using _Project.Scripts.Entities;
using _Project.Scripts.Entities.Asteroids;
using _Project.Scripts.Entities.UFO;
using _Project.Scripts.Level.GameSession;
using _Project.Scripts.Player;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services
{
    public class LevelBootstrap : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private bool isCursorVisible;

        private DiContainer _di;
        private IInput _inputHandler;
        private EntitiesContainer _entitiesContainer;
        private PlayerStatePresenter _playerStatePresenter;
        private PauseService _pauseService;
        private AsteroidsController _asteroidsController;
        private UfosSpawner _ufosSpawner;
        private CursorService _cursorService;
        private UfosController _ufosController;
        private GameSessionModel _gameSessionModel;


        [Inject]
        private void Construct(DiContainer diContainer, IInput inputHandler,
            EntitiesContainer entitiesContainer, PlayerStatePresenter playerStatePresenter,
            AsteroidsController asteroidsController, CursorService cursorService,
            UfosSpawner ufosSpawner, UfosController ufosController, GameSessionModel gameSessionModel,
            PauseService pauseService = null)
        {
            _di = diContainer;
            _inputHandler = inputHandler;
            _entitiesContainer = entitiesContainer;
            _playerStatePresenter = playerStatePresenter;
            _asteroidsController = asteroidsController;
            _cursorService = cursorService;
            _pauseService = pauseService;
            _ufosSpawner = ufosSpawner;
            _ufosController = ufosController;
            _gameSessionModel = gameSessionModel;
        }

        private void Awake()
        {
            GameObject player = _di.InstantiatePrefab(playerPrefab);
            player.transform.position = playerSpawnPoint.position;
            IPlayerMovement playerMovement = player.GetComponent<IPlayerMovement>();
            _playerStatePresenter.SetPlayerModel(playerMovement);

            _entitiesContainer.AddEntity(player.GetComponent<Entity>());

            _pauseService?.AddItem(_entitiesContainer);
            _pauseService?.AddItem(_asteroidsController);
            _pauseService?.AddItem(_ufosSpawner);

            _ufosController.SetTarget(player.GetComponent<IEnemyTargetable>());
            _gameSessionModel.SetPlayer(player.GetComponent<PlayerHealth>());
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