using System.Collections;
using UnityEngine;
using Zenject;

namespace Player.Weapons.Laser
{
    public class LaserShooter : Weapon
    {
        [SerializeField] private Laser laser;

        private LaserModel _model;

        private Coroutine _cooldownRoutine;
        private Coroutine _laserActiveRoutine;

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

        public override void TryShoot()
        {
            if (_model.IsNoChargesLeft)
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

        private IEnumerator LaserDisableRoutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            laser.Disable();
        }

        private IEnumerator CooldownRoutine(float cooldown)
        {
            float timer = cooldown;
            _model.SetIsOnCooldown(true);

            while (timer > 0f)
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


                yield return null;
            }


            _model.SetIsOnCooldown(false);
        }

        public override void SetEnable(bool isEnabled)
        {
            _model.ResetCharges();
        }
    }
}