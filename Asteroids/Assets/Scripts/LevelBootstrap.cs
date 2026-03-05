using Player;
using UnityEngine;
using Zenject;

public class LevelBootstrap : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerSpawnPoint;

    private DiContainer _di;
    private LevelBounds _levelBounds;
    private IInput _inputHandler;

    [Inject]
    private void Construct(LevelBounds bounds, DiContainer diContainer, IInput inputHandler)
    {
        _levelBounds = bounds;
        _di = diContainer;
        _inputHandler = inputHandler;
    }

    private GameObject _player;

    private void Awake()
    {
        _player = _di.InstantiatePrefab(playerPrefab);
        _player.transform.position = playerSpawnPoint.position;
        _levelBounds.Init(_player.transform);
    }

    private void Start()
    {
        _inputHandler.Enable();
    }
}