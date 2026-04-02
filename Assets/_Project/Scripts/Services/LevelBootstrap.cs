using _Project.Scripts.Entities;
using _Project.Scripts.Entities.UFO;
using _Project.Scripts.Level.BoundsHandling;
using _Project.Scripts.Level.GameSession;
using _Project.Scripts.Player;
using _Project.Scripts.Services.Pause;
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
        private CursorService _cursorService;
        private UfosController _ufosController;
        private GameOverModel _gameOverModel;
        private LevelBounds _levelBounds;


        [Inject]
        private void Construct(DiContainer diContainer,
            IInput inputHandler,
            EntitiesContainer entitiesContainer,
            PlayerStatePresenter playerStatePresenter,
            CursorService cursorService,
            UfosController ufosController,
            GameOverModel gameOverModel,
            LevelBounds levelBounds,
            PauseService pauseService = null)
        {
            _di = diContainer;
            _inputHandler = inputHandler;
            _entitiesContainer = entitiesContainer;
            _playerStatePresenter = playerStatePresenter;
            _cursorService = cursorService;
            _ufosController = ufosController;
            _gameOverModel = gameOverModel;
            _levelBounds = levelBounds;
        }

        private void Awake()
        {
            GameObject player = _di.InstantiatePrefab(playerPrefab);
            
            player.transform.position = playerSpawnPoint.position;
            _playerStatePresenter.SetPlayerModel(player.GetComponent<PlayerMovement>());
            if (player.TryGetComponent(out LevelBoundsHandler levelBoundsHandler))
                levelBoundsHandler.Initialize(_levelBounds);
            _entitiesContainer.AddEntity(player.GetComponent<Entity>());
            
            _ufosController.SetTarget(player.GetComponent<EnemyTarget>());
            _gameOverModel.SetPlayer(player.GetComponent<Player.Player>());
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