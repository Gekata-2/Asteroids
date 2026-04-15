namespace _Project.Scripts.Player.Weapons.MachineGun
{
    public class MachineGunModel
    {
        private readonly MachineGunConfig _config;
        
        public int ShotsFired { get; private set; }
        public bool CanShoot { get; private set; }
        
        public float FireCooldown => _config.FireCooldown;
        public float BulletSpeed => _config.BulletSpeed;
        public float BulletLifeTime => _config.BulletLifeTime;
        
        public MachineGunModel(PlayerWeaponsConfig weaponsConfig)
        {
            _config = weaponsConfig.MachineGun;
        }

        public void SetIsCanShoot(bool canShoot)
            => CanShoot = canShoot;

        public void IncreaseShotsFired()
            => ShotsFired++;
    }
}