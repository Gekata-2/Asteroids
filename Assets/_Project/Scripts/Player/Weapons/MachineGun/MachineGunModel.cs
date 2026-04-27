using _Project.Scripts.Services.RemoteConfigs;

namespace _Project.Scripts.Player.Weapons.MachineGun
{
    public class MachineGunModel : IConfigFetcher
    {
        private MachineGunConfig _config;

        public int ShotsFired { get; private set; }
        public bool CanShoot { get; private set; }

        public float FireCooldown => _config.FireCooldown;
        public float BulletSpeed => _config.BulletSpeed;
        public float BulletLifeTime => _config.BulletLifeTime;

        public void SetIsCanShoot(bool canShoot)
            => CanShoot = canShoot;

        public void IncreaseShotsFired()
            => ShotsFired++;

        public void FetchConfig(IConfigsProvider configsProvider)
        {
            PlayerWeaponsConfig weaponsConfig =
                configsProvider.GetValue<PlayerWeaponsConfig>(ConfigsNames.PlayerWeapons);
            _config = weaponsConfig.MachineGun;
        }
    }
}