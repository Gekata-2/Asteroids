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
        [Header("UI")] 
        [SerializeField] private ScoreView scoreViewPrefab;
        [SerializeField] private PlayerStateView playerStateViewPrefab;
        [SerializeField] private LaserView laserViewPrefab;

        public override void InstallBindings()
        {
            Container.Bind<PlayerConfig>().FromScriptableObject(playerConfig).AsSingle();
            Container.Bind<PlayerWeaponsConfig>().FromScriptableObject(weaponsConfig).AsSingle();

            Container.Bind<PlayerInputActionMap>().FromNew().AsSingle();
            Container.Bind<IInput>().To<InputHandler>().FromNew().AsSingle();

            Container.Bind<PlayerModel>().To<PlayerState>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerDataController>().FromNew().AsSingle();

            Container.Bind<LaserModel>().To<SimpleLaserModel>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<LaserController>().FromNew().AsSingle();
            
            Container.Bind<ScoreView>().FromComponentInNewPrefab(scoreViewPrefab).AsSingle();
            Container.Bind<PlayerStateView>().FromComponentInNewPrefab(playerStateViewPrefab).AsSingle();
            Container.Bind<LaserView>().FromComponentInNewPrefab(laserViewPrefab).AsSingle();
        }
    }
}