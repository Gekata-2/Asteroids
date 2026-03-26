using System.Collections;
using _Project.Scripts.Services.Pause;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Player.Weapons.Laser
{
    public class LaserShooter : MonoBehaviour, IPausable
    {
        [SerializeField] private Laser laser;

        private LaserModel _model;

        private Coroutine _cooldownRoutine;
        private Coroutine _laserActiveRoutine;

        private bool _isPaused;

        [Inject]
        private void Construct(LaserModel model)
        {
            _model = model;
        }

        private void Start()
        {
            laser.SetLength(_model.Lenght);
            laser.Disable();
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
            if (_cooldownRoutine != null)
                StopCoroutine(_cooldownRoutine);

            _cooldownRoutine = StartCoroutine(CooldownRoutine(_model.Cooldown));
        }

        private void ShowLaser()
        {
            if (!laser.IsEnabled)
                laser.Enable();
            if (_laserActiveRoutine != null)
                StopCoroutine(_laserActiveRoutine);

            _laserActiveRoutine = StartCoroutine(LaserDisableRoutine(_model.Duration));
        }

        private IEnumerator CooldownRoutine(float cooldown)
        {
            float timer = cooldown;
            _model.SetIsOnCooldown(true);

            while (timer > 0f)
            {
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

                yield return null;
            }

            _model.SetIsOnCooldown(false);
        }

        private IEnumerator LaserDisableRoutine(float duration)
        {
            float timer = duration;
            while (timer >= 0f)
            {
                if (!_isPaused)
                    timer -= Time.deltaTime;

                yield return null;
            }

            laser.Disable();
        }

        public void Pause()
            => _isPaused = true;

        public void Resume()
            => _isPaused = false;
    }
}