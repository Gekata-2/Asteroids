using System.Collections.Generic;
using System.Threading;
using _Project.Scripts.Extensions;
using _Project.Scripts.Services.Pause;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace _Project.Scripts.Player.Weapons.MachineGun
{
    public class MachineGunShooter : MonoBehaviour, IPausable
    {
        [SerializeField] private Transform _bulletsOrigin;
        [SerializeField] private Bullet _bulletPrefab;

        [Header("Bullets Pool")] 
        [SerializeField, Min(1)] private int _maxSize = 60;
        [SerializeField, Min(1)] private int _defaultCapacity = 30;
        
        private bool _isPaused;

        private MachineGunModel _model;
        private PauseService _pauseService;
        
        private ObjectPool<Bullet> _bulletsPool;
        private List<Bullet> _activeBullets;

        private CancellationTokenSource _cooldownCts;

        [Inject]
        private void Construct(MachineGunModel model, PauseService pauseService)
        {
            _model = model;
            _pauseService = pauseService;
        }

        private void Awake()
        {
            _activeBullets = new List<Bullet>();
            _bulletsPool = new ObjectPool<Bullet>(
                CreateBullet,
                OnTakeBulletFromPool,
                OnReturnBulletToPool,
                OnDestroyBullet,
                collectionCheck: true,
                defaultCapacity: _defaultCapacity,
                maxSize: _maxSize
            );
        }

        private void Start()
        {
            _bulletsPool.PreWarm(_defaultCapacity);
        }

        private void OnDestroy()
        {
            _cooldownCts?.Cancel();
            _cooldownCts?.Dispose();

            _bulletsPool.Clear();
            _activeBullets.Clear();
        }

        private void OnDestroyBullet(Bullet bullet)
        {
            UnsubscribeFromBullet(bullet);
            _pauseService?.RemoveItem(bullet);
            Destroy(bullet.gameObject);
        }

        private void OnReturnBulletToPool(Bullet bullet)
        {
            UnsubscribeFromBullet(bullet);
            bullet.gameObject.SetActive(false);
            if (_activeBullets.Contains(bullet))
                _activeBullets.Remove(bullet);
        }

        private void UnsubscribeFromBullet(Bullet bullet)
        {
            bullet.Collided -= OnBulletCollided;
            bullet.LiveTimeEnded -= OnBulletLiveTimeEnded;
        }

        private void OnTakeBulletFromPool(Bullet bullet)
        {
            bullet.Collided += OnBulletCollided;
            bullet.LiveTimeEnded += OnBulletLiveTimeEnded;
            bullet.gameObject.SetActive(true);
            if (!_activeBullets.Contains(bullet))
                _activeBullets.Add(bullet);
        }

        private Bullet CreateBullet()
        {
            Bullet bullet = Instantiate(_bulletPrefab, _bulletsOrigin);
            _pauseService?.AddItem(bullet);
            return bullet;
        }

        private void OnBulletCollided(Bullet bullet)
        {
            _bulletsPool.Release(bullet);
        }

        private void OnBulletLiveTimeEnded(Bullet bullet)
        {
            _bulletsPool.Release(bullet);
        }

        public virtual void TryShoot()
        {
            if (!_model.CanShoot || _isPaused)
                return;

            _cooldownCts?.Cancel();
            _cooldownCts?.Dispose();
            _cooldownCts = new CancellationTokenSource();


            Bullet bullet = _bulletsPool.Get();
            Transform bulletTransform = bullet.transform;

            (bulletTransform.position, bulletTransform.rotation) = (_bulletsOrigin.position, _bulletsOrigin.rotation);
            bullet.Initialize(new BulletData(_model.BulletSpeed, bulletTransform.up, _model.BulletLifeTime));
            
            _model.IncreaseShotsFired();

            CooldownRoutine(_model.FireCooldown, _cooldownCts.Token).Forget();
        }


        private async UniTask CooldownRoutine(float cooldown, CancellationToken token)
        {
            _model.SetIsCanShoot(false);
            float timer = cooldown;

            while (timer >= 0f)
            {
                token.ThrowIfCancellationRequested();

                if (!_isPaused)
                    timer -= Time.deltaTime;

                await UniTask.Yield(token);
            }

            _model.SetIsCanShoot(true);
        }

        public void Enable()
            => _model.SetIsCanShoot(true);

        public void Pause()
            => _isPaused = true;

        public void Resume()
            => _isPaused = false;
    }
}