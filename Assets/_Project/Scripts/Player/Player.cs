using System;
using System.Threading;
using _Project.Scripts.Entities;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class Player : MonoBehaviour, IDamageVisitable
    {
        public event Action PlayerDead;

        [SerializeField] private bool _isActive = true;

        private CancellationTokenSource _ctsImmunity;
        private bool _isImmuneToDamage;

        private void OnDestroy()
        {
            _ctsImmunity?.Cancel();
            _ctsImmunity?.Dispose();
        }

        public void Die()
        {
            if (_isActive && !_isImmuneToDamage)
                PlayerDead?.Invoke();
        }

        public void Accept(IDamageVisitor visitor)
            => visitor.Visit(this);

        public void ResetLife(float immunityTime)
        {
            _ctsImmunity?.Cancel();
            _ctsImmunity?.Dispose();
            _ctsImmunity = new CancellationTokenSource();

            ImmunityRoutine(immunityTime).Forget();
        }

        private async UniTask ImmunityRoutine(float time)
        {
            _isImmuneToDamage = true;
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: _ctsImmunity.Token);
            _isImmuneToDamage = false;
        }
    }
}