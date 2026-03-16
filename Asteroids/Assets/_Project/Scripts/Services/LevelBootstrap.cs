using _Project.Scripts.Entities;
using _Project.Scripts.Entities.Asteroids;
using _Project.Scripts.Entities.UFO;
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
        private PlayerModel _playerModel;
        private PauseService _pauseService;
        private AsteroidsSpawner _asteroidsSpawner;
        private UfosSpawner _ufosSpawner;
        private CursorService _cursorService;
        private UfosController _ufosController;
        private GameObject _player;

        [Inject]
        private void Construct(DiContainer diContainer, IInput inputHandler,
            EntitiesContainer entitiesContainer, PlayerModel playerModel,
            AsteroidsSpawner asteroidsSpawner, CursorService cursorService,
            UfosSpawner ufosSpawner, UfosController ufosController,
            PauseService pauseService = null)
        {
            _di = diContainer;
            _inputHandler = inputHandler;
            _entitiesContainer = entitiesContainer;
            _playerModel = playerModel;
            _asteroidsSpawner = asteroidsSpawner;
            _cursorService = cursorService;
            _pauseService = pauseService;
            _ufosSpawner = ufosSpawner;
            _ufosController = ufosController;
        }

        private void Awake()
        {
            _player = _di.InstantiatePrefab(playerPrefab);
            _player.transform.position = playerSpawnPoint.position;
            IPlayerMovement playerMovement = _player.GetComponent<IPlayerMovement>();
            _playerModel.SetPlayer(playerMovement);

            _entitiesContainer.AddEntity(_player.GetComponent<Entity>());

            _pauseService?.AddItem(_entitiesContainer);
            _pauseService?.AddItem(_asteroidsSpawner);
            _pauseService?.AddItem(_ufosSpawner);

            _ufosController.SetTarget(_player.GetComponent<IEnemyTargetable>());
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