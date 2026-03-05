using Player;
using UnityEngine;

public class LevelBootstrap : MonoBehaviour
{
    [SerializeField] private PlayerConfig playerConfig;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerSpawnPoint;

    private GameObject _player;
    private PlayerInputActionMap _inputActionMap;
    private InputHandler _inputHandler;

    private void Awake()
    {
        _player = Instantiate(playerPrefab);
        _player.transform.position = playerSpawnPoint.position;
        _inputActionMap = new PlayerInputActionMap();
        _inputHandler = new InputHandler(_inputActionMap);
    }

    private void Start()
    {
        PlayerMovement movement = _player.GetComponent<PlayerMovement>();
        movement.Init(_inputHandler, playerConfig);
        _inputHandler.Enable();
    }
}