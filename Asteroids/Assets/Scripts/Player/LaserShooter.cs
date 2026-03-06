using System.Collections;
using UnityEngine;
using Zenject;

namespace Player
{
    public class LaserShooter : Weapon
    {
        [SerializeField] private Laser laser;

        private PlayerWeaponsConfig.LaserConfig _config;

        private Coroutine _cooldownRoutine;
        private Coroutine _laserActiveRoutine;
        private LaserCharges _charges;

        [Inject]
        private void Construct(PlayerWeaponsConfig weaponsConfig)
        {
            _config = weaponsConfig.Laser;
        }

        private void Start()
        {
            laser.SetLength(_config.Lenght);
            laser.Disable();

            _charges = new LaserCharges(_config.Charges);
        }

        public override void TryShoot()
        {
            if (_charges.IsZero())
                return;

            if (_cooldownRoutine != null)
                StopCoroutine(_cooldownRoutine);
            if (_laserActiveRoutine != null)
                StopCoroutine(_laserActiveRoutine);

            if (!laser.IsEnabled) 
                laser.Enable();

            _charges.UseCharge();

            _laserActiveRoutine = StartCoroutine(LaserDisableRoutine());
            _cooldownRoutine = StartCoroutine(CooldownRoutine(_config.Cooldown));
        }

        private IEnumerator LaserDisableRoutine()
        {
            yield return new WaitForSeconds(_config.Duration);
            laser.Disable();
        }

        private IEnumerator CooldownRoutine(float cooldown)
        {
            yield return new WaitForSeconds(cooldown);
            _charges.RestoreCharge();
        }

        public override void SetEnable(bool isEnabled)
        {
            _charges.ResetCharges();
        }
    }
}