using System.Threading;
using _Project.Scripts.Services.Pause;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Player.Weapons.Laser
{
    public class LaserShooter : MonoBehaviour, IPausable
    {
        [SerializeField] private Laser _laser;

        private LaserModel _model;

        private bool _isPaused;

        private CancellationTokenSource _ctsCooldown;
        private CancellationTokenSource _ctsLaser;

        [Inject]
        private void Construct(LaserModel model)
        {
            _model = model;
        }

        private void Start()
        {
            _laser.SetLength(_model.Lenght);
            _laser.Disable();
        }

        private void OnDestroy()
        {
            _ctsCooldown?.Cancel();
            _ctsCooldown?.Dispose();
            _ctsLaser?.Cancel();
            _ctsLaser?.Dispose();
        }

        public virtual void TryShoot()
        {
            if (_model.IsNoChargesLeft || _isPaused)
                return;

            _model.UseCharge();

            ShowLaser();
            StartCooldown();
        }

        private void StartCooldown()
        {
            if (_model.IsOnCooldown)
                return;

            _ctsCooldown?.Cancel();
            _ctsCooldown?.Dispose();
            _ctsCooldown = new CancellationTokenSource();

            CooldownRoutine(_model.Cooldown, _ctsCooldown.Token).Forget();
        }

        private void ShowLaser()
        {
            if (!_laser.IsEnabled)
                _laser.Enable();

            _ctsLaser?.Cancel();
            _ctsLaser?.Dispose();
            _ctsLaser = new CancellationTokenSource();

            LaserDisableRoutine(_model.Duration, _ctsLaser.Token).Forget();
        }

        private async UniTask CooldownRoutine(float cooldown, CancellationToken token)
        {
            float timer = cooldown;
            _model.SetIsOnCooldown(true);

            while (timer > 0f)
            {
                token.ThrowIfCancellationRequested();
                if (!_isPaused)
                {
                    if (timer - Time.deltaTime <= 0)
                    {
                        _model.RestoreCharge();
                        _model.SetCooldownTimeLeft(0f);

                        if (!_model.IsChargesFull)
                            timer = cooldown;
                    }

                    timer -= Time.deltaTime;
                    _model.SetCooldownTimeLeft(timer);
                }

                await UniTask.Yield(token);
            }

            _model.SetIsOnCooldown(false);
        }


        private async UniTask LaserDisableRoutine(float duration, CancellationToken token)
        {
            float timer = duration;
            while (timer >= 0f)
            {
                token.ThrowIfCancellationRequested();
                if (!_isPaused)
                    timer -= Time.deltaTime;

                await UniTask.Yield(token);
            }

            _laser.Disable();
        }

        public void Pause()
            => _isPaused = true;

        public void Resume()
            => _isPaused = false;
    }
}