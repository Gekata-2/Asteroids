using _Project.Scripts.Level.GameSession;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.Player.Weapons.Laser;
using _Project.Scripts.Player.Weapons.MachineGun;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private PlayerWeaponsConfig _weaponsConfig;


        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerFactory>().AsSingle();

            Container.Bind<PlayerConfig>().FromScriptableObject(_playerConfig).AsSingle();
            Container.Bind<PlayerWeaponsConfig>().FromScriptableObject(_weaponsConfig).AsSingle();

            Container.Bind<PlayerInputActionMap>().AsSingle();
            Container.Bind<IInput>().To<InputHandler>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerStatePresenter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerScorePresenter>().AsSingle();

            Container.Bind<MachineGunModel>().AsSingle();

            Container.Bind<LaserModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<LaserPresenter>().AsSingle();

            Container.Bind<AssetsFactory>().AsSingle();
        }
    }
}