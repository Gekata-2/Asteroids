using _Project.Scripts.Player.Weapons.Configs;
using _Project.Scripts.Services.RemoteConfigs;
using Zenject;

namespace _Project.Scripts.Player.Weapons.MachineGun
{
    public class MachineGunModel : IInitializable
    {
        public int ShotsFired { get; private set; }
        public bool CanShoot { get; private set; }

        public float FireCooldown => _config.FireCooldown;
        public float BulletSpeed => _config.BulletSpeed;
        public float BulletLifeTime => _config.BulletLifeTime;

        private readonly IConfigsProvider _configsProvider;
        private MachineGunConfig _config;

        public MachineGunModel(IConfigsProvider configsProvider)
        {
            _configsProvider = configsProvider;
        }

        public void Initialize()
        {
            PlayerWeaponsConfig weaponsConfig =
                _configsProvider.GetValue<PlayerWeaponsConfig>(ConfigsNames.PlayerWeapons);
            _config = weaponsConfig.MachineGun;
        }

        public void SetIsCanShoot(bool canShoot)
            => CanShoot = canShoot;

        public void IncreaseShotsFired()
            => ShotsFired++;
    }
}