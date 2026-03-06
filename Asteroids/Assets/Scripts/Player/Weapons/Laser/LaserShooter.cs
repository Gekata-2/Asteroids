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

            if (_cooldownRoutine != null)
                StopCoroutine(_cooldownRoutine);
            if (_laserActiveRoutine != null)
                StopCoroutine(_laserActiveRoutine);

            if (!laser.IsEnabled)
                laser.Enable();

            _model.UseCharge();

            _laserActiveRoutine = StartCoroutine(LaserDisableRoutine(_model.Duration));
            _cooldownRoutine = StartCoroutine(CooldownRoutine(_model.Cooldown));
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
                timer -= Time.deltaTime;
                _model.SetCooldownTimeLeft(timer);
                yield return null;
            }

            _model.SetCooldownTimeLeft(0f);
            _model.SetIsOnCooldown(false);
            _model.RestoreCharge();
        }

        public override void SetEnable(bool isEnabled)
        {
            _model.ResetCharges();
        }
    }
}