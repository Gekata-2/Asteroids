using Zenject;

namespace _Project.Scripts.Player.Weapons.Laser
{
    public class LaserController : IInitializable
    {
        private readonly LaserModel _model;
        private readonly PlayerWeaponsConfig _playerWeaponsConfig;

        public LaserController(LaserModel model, PlayerWeaponsConfig playerWeaponsConfig)
        {
            _model = model;
            _playerWeaponsConfig = playerWeaponsConfig;
        }

        public void Initialize()
        {
            _model.SetConfig(_playerWeaponsConfig.Laser);
        }
    }
}