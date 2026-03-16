using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.Player.Weapons.Laser;
using _Project.Scripts.UI;
using _Project.Scripts.UI.Laser;
using UnityEngine;
using Zenject;

namespace _Project.Scripts._Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private PlayerWeaponsConfig weaponsConfig;

        public override void InstallBindings()
        {
            Container.Bind<PlayerConfig>().FromScriptableObject(playerConfig).AsSingle().NonLazy();
            Container.Bind<PlayerWeaponsConfig>().FromScriptableObject(weaponsConfig).AsSingle().NonLazy();
            Container.Bind<PlayerInputActionMap>().FromNew().AsSingle().NonLazy();
            Container.Bind<IInput>().To<InputHandler>().FromNew().AsSingle().NonLazy();


            Container.Bind<PlayerModel>().To<PlayerState>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerDataController>().FromNew().AsSingle().NonLazy();
            Container.Bind<PlayerStateView>().To<TextPlayerStateView>().FromComponentsInHierarchy().AsSingle()
                .NonLazy();
            Container.Bind<ScoreView>().To<TextScoreView>().FromComponentsInHierarchy().AsSingle().NonLazy();


            Container.Bind<LaserModel>().To<SimpleLaserModel>().FromNew().AsSingle().NonLazy();
            Container.Bind<LaserView>().To<LaserIconView>().FromComponentsInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LaserController>().FromNew().AsSingle().NonLazy();
        }
    }
}