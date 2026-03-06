using DefaultNamespace;
using Entities;
using Player;
using UnityEngine;
using Zenject;

public class LevelBootstrap : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerSpawnPoint;

    private DiContainer _di;
    private IInput _inputHandler;
    private EntitiesContainer _entitiesContainer;
    private PlayerModel _playerModel;
    
    private GameObject _player;
    
    [Inject]
    private void Construct(DiContainer diContainer, IInput inputHandler,
        EntitiesContainer entitiesContainer, PlayerModel playerModel)
    {
        _di = diContainer;
        _inputHandler = inputHandler;
        _entitiesContainer = entitiesContainer;
        _playerModel = playerModel;
    }
    
    private void Awake()
    {
        _player = _di.InstantiatePrefab(playerPrefab);
        _player.transform.position = playerSpawnPoint.position;
        _entitiesContainer.AddEntity(_player.GetComponent<Entity>());
        _playerModel.SetPlayer(_player.GetComponent<IPlayerMovement>());
    }

    private void Start()
    {
        _inputHandler.Enable();
    }

    private void OnDestroy()
    {
        _inputHandler.Disable();
    }
}