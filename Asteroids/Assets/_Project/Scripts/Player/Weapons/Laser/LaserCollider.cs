using System;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons.Laser
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