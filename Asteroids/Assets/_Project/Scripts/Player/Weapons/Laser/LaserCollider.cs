using System;
using UnityEngine;

namespace Player.Weapons.Laser
{
    public class LaserCollider : MonoBehaviour
    {
        public event Action<Collider2D> TriggerEnter;

        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEnter?.Invoke(other);
        }
    }
}