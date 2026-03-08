using Player.Weapons.Laser;
using Player.Weapons.MachineGun;
using Services;
using UnityEngine;
using Zenject;

namespace Player.Weapons
{
    public class WeaponsController : MonoBehaviour
    {
        [SerializeField] private MachineGunShooter machineGunShooter;
        [SerializeField] private LaserShooter laserShooter;

        private IInput _input;
        private PauseService _pauseService;
        private bool _isMachineGunShootPressed;

        [Inject]
        private void Construct(IInput input, PauseService pauseService)
        {
            _input = input;
            _pauseService = pauseService;
        }

        private void Awake()
        {
            if (_pauseService != null)
            {
                _pauseService.AddItem(laserShooter);
                _pauseService.AddItem(machineGunShooter);
            }
        }

        private void Update()
        {
            if (_isMachineGunShootPressed) 
                machineGunShooter.TryShoot();
        }

        private void Start()
        {
            _input.ShootMachineGunPerformed += Input_OnShootMachineGunPerformed;
            _input.ShootMachineGunCanceled += Input_OnShootMachineGunCanceled;
            _input.ShootLaserPerformed += Input_OnShootLaserPerformed;

            machineGunShooter.Enable();
        }

        private void OnDestroy()
        {
            _input.ShootMachineGunPerformed -= Input_OnShootMachineGunPerformed;
            _input.ShootMachineGunCanceled -= Input_OnShootMachineGunCanceled;
            _input.ShootLaserPerformed -= Input_OnShootLaserPerformed;
        }

        private void Input_OnShootLaserPerformed()
        {
            laserShooter.TryShoot();
        }

        private void Input_OnShootMachineGunCanceled()
        {
            _isMachineGunShootPressed = false;
        }

        private void Input_OnShootMachineGunPerformed()
        {
            _isMachineGunShootPressed = true;
        }
    }
}