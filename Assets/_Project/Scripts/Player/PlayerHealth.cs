using System;
using _Project.Scripts.Entities;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageble
    {
        public event Action<Damage> PlayerDead;

        [SerializeField] private bool isActive = true;
        

        public void TakeDamage(Damage damage)
        {
            if (isActive) 
                PlayerDead?.Invoke(damage);
        }
    }
}