using _Project.Scripts.Player.Weapons.Laser;
using _Project.Scripts.Player.Weapons.MachineGun;
using _Project.Scripts.Services.Pause;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Player.Weapons
{
    public class WeaponsController : MonoBehaviour
    {
        [SerializeField] private MachineGunShooter _machineGunShooter;
        [SerializeField] private LaserShooter _laserShooter;

        private IInput _input;
        private PauseService _pauseService;
        private bool _isMachineGunShootPressed;

        [Inject]
        private void Construct(IInput input, PauseService pauseService = null)
        {
            _input = input;
            _pauseService = pauseService;
        }

        private void Awake()
        {
            if (_pauseService != null)
            {
                _pauseService.AddItem(_laserShooter);
                _pauseService.AddItem(_machineGunShooter);
            }
        }

        private void Start()
        {
            _input.ShootMachineGunPerformed += OnShootMachineGunPerformed;
            _input.ShootMachineGunCanceled += OnShootMachineGunCanceled;
            _input.ShootLaserPerformed += OnShootLaserPerformed;
            
            _machineGunShooter.Enable();
        }

        private void OnDestroy()
        {
            _input.ShootMachineGunPerformed -= OnShootMachineGunPerformed;
            _input.ShootMachineGunCanceled -= OnShootMachineGunCanceled;
            _input.ShootLaserPerformed -= OnShootLaserPerformed;
        }

        private void Update()
        {
            if (_isMachineGunShootPressed)
                _machineGunShooter.TryShoot();
        }

        public void ResetLaser()
            => _laserShooter.RestoreAllCharges();
        
        private void OnShootLaserPerformed()
            => _laserShooter.TryShoot();

        private void OnShootMachineGunCanceled()
            => _isMachineGunShootPressed = false;

        private void OnShootMachineGunPerformed()
            => _isMachineGunShootPressed = true;
        
    }
}