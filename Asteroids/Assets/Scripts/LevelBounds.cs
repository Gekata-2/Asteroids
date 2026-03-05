using System;
using UnityEngine;

public class LevelBounds : MonoBehaviour
{
    [SerializeField] private float size = 3f;
    [SerializeField] private bool drawGizmos;

    private Transform _player;
    private Bounds _bounds;

    private void Start()
    {
        _bounds = new Bounds(Vector3.zero, Vector3.one * size);
    }

    public void Init(Transform player)
    {
        _player = player;
    }

    private void Update()
    {
        if (IsPlayerOutOfBounds())
        {
          
        }
    }
    

    private bool IsPlayerOutOfBounds()
    {
        return !_bounds.Contains(_player.position);
    }

    private void OnDrawGizmos()
    {
        if (!drawGizmos)
            return;
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_bounds.center, _bounds.size);
    }
}