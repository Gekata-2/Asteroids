using Asteroids;
using Entities;
using Player;
using UnityEngine;
using Zenject;

namespace Services
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
        private CursorService _cursorService;
        private GameObject _player;

        [Inject]
        private void Construct(DiContainer diContainer, IInput inputHandler,
            EntitiesContainer entitiesContainer, PlayerModel playerModel,
            AsteroidsSpawner asteroidsSpawner, CursorService cursorService,
            PauseService pauseService = null)
        {
            _di = diContainer;
            _inputHandler = inputHandler;
            _entitiesContainer = entitiesContainer;
            _playerModel = playerModel;
            _asteroidsSpawner = asteroidsSpawner;
            _cursorService = cursorService;
            _pauseService = pauseService;
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