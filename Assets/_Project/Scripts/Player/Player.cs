using System;
using _Project.Scripts.Entities;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class Player : MonoBehaviour, IDamageVisitable
    {
        public event Action PlayerDead;

        [SerializeField] private bool _isActive = true;

        public void Die()
        {
            if (_isActive)
                PlayerDead?.Invoke();
        }

        public void Accept(IDamageVisitor visitor) 
            => visitor.Visit(this);
    }
}