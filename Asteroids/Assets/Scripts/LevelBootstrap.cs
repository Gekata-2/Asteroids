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

    [Inject]
    private void Construct(DiContainer diContainer, IInput inputHandler,
        EntitiesContainer entitiesContainer)
    {
        _di = diContainer;
        _inputHandler = inputHandler;
        _entitiesContainer = entitiesContainer;
    }

    private GameObject _player;

    private void Awake()
    {
        _player = _di.InstantiatePrefab(playerPrefab);
        _player.transform.position = playerSpawnPoint.position;
        _entitiesContainer.AddEntity(_player.GetComponent<Entity>());
    }

    private void Start()
    {
        _inputHandler.Enable();
    }
}