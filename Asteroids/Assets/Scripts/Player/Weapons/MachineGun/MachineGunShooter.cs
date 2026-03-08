using System.Collections;
using UnityEngine;
using Zenject;

namespace Player.Weapons.MachineGun
{
    public class MachineGunShooter : Weapon
    {
        [SerializeField] private Transform bulletsOrigin;
        [SerializeField] private Bullet bulletPrefab;

        private PlayerWeaponsConfig _weaponsConfig;
        private bool _canShoot;
        private Coroutine _cooldownRoutine;
        private bool _isPaused;

        [Inject]
        private void Construct(PlayerWeaponsConfig weaponsConfig)
        {
            _weaponsConfig = weaponsConfig;
        }

        public void Enable()
            => _canShoot = true;

        public override void TryShoot()
        {
            if (!_canShoot || _isPaused)
                return;
            if (_cooldownRoutine != null)
                StopCoroutine(_cooldownRoutine);

            Bullet bullet = Instantiate(bulletPrefab);
            Transform bulletTransform = bullet.transform;

            (bulletTransform.position, bulletTransform.rotation) = (bulletsOrigin.position, bulletsOrigin.rotation);

            bullet.SetSpeed(_weaponsConfig.MachineGun.BulletSpeed);
            bullet.SetDirection(bullet.transform.up);

            _cooldownRoutine = StartCoroutine(CooldownRoutine(_weaponsConfig.MachineGun.FireCooldown));
        }

        private IEnumerator CooldownRoutine(float cooldown)
        {
            _canShoot = false;
            float timer = cooldown;
            while (timer >= 0f)
            {
                if (!_isPaused)
                {
                    timer -= Time.deltaTime;
                }

                yield return null;
            }

            _canShoot = true;
        }

        public override void SetEnable(bool isEnabled)
        {
        }

        public override void Pause() => _isPaused = true;

        public override void Resume() => _isPaused = false;
    }
}