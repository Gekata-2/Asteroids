using System;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons.Laser
{
    public class LaserCollider : MonoBehaviour
    {
        public event Action<Collider2D> TriggerEntered;

        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEntered?.Invoke(other);
        }
    }
}