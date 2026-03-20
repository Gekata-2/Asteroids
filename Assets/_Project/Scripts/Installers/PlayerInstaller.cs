using _Project.Scripts.Level.GameSession;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.Player.Weapons.Laser;
using _Project.Scripts.UI;
using _Project.Scripts.UI.Laser;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
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

            Container.Bind<PlayerInputActionMap>().AsSingle();
            Container.Bind<IInput>().To<InputHandler>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerStatePresenter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerScorePresenter>().AsSingle();

            Container.Bind<LaserModel>().To<SimpleLaserModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<LaserController>().AsSingle();

            Container.Bind<ScoreView>().FromComponentInNewPrefab(scoreViewPrefab).AsSingle();
            Container.Bind<PlayerStateView>().FromComponentInNewPrefab(playerStateViewPrefab).AsSingle();
            Container.Bind<LaserView>().FromComponentInNewPrefab(laserViewPrefab).AsSingle();
        }
    }
}