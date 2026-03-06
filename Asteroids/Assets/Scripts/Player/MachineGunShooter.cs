using System.Collections;
using UnityEngine;
using Zenject;

namespace Player
{
    public class MachineGunShooter : MonoBehaviour
    {
        [SerializeField] private Transform bulletsOrigin;
        [SerializeField] private Bullet bulletPrefab;

        private PlayerWeaponsConfig _weaponsConfig;
        private bool _canShoot;
        private Coroutine _cooldownRoutine;

        [Inject]
        private void Construct(PlayerWeaponsConfig weaponsConfig)
        {
            _weaponsConfig = weaponsConfig;
        }

        public void Enable()
            => _canShoot = true;

        public void TryShoot()
        {
            if (!_canShoot)
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
            yield return new WaitForSeconds(cooldown);
            _canShoot = true;
        }
    }
}