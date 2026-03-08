using System;
using UnityEngine;

public class LaserCollider : MonoBehaviour
{
    public event Action<Collider2D> TriggerEnter;

    private void OnTriggerEnter2D(Collider2D other)
    {
       TriggerEnter?.Invoke(other);
    }
}