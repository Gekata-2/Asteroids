using _Project.Scripts.Level.GameSession;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.Player.Weapons.Laser;
using _Project.Scripts.Player.Weapons.MachineGun;
using _Project.Scripts.UI;
using _Project.Scripts.UI.Laser;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private PlayerWeaponsConfig _weaponsConfig;
        [Header("UI")] 
        [SerializeField] private ScoreView _scoreViewPrefab;
        [SerializeField] private PlayerStateView _playerStateViewPrefab;
        [SerializeField] private LaserView _laserViewPrefab;

        public override void InstallBindings()
        {
            Container.BindFactory<Player.Player, PlayerFactory>().FromComponentInNewPrefab(_playerPrefab);
            
            Container.Bind<PlayerConfig>().FromScriptableObject(_playerConfig).AsSingle();
            Container.Bind<PlayerWeaponsConfig>().FromScriptableObject(_weaponsConfig).AsSingle();

            Container.Bind<PlayerInputActionMap>().AsSingle();
            Container.Bind<IInput>().To<InputHandler>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerStatePresenter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerScorePresenter>().AsSingle();

            Container.Bind<MachineGunModel>().AsSingle();
            
            Container.Bind<LaserModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<LaserPresenter>().AsSingle();

            Container.Bind<ScoreView>().FromComponentInNewPrefab(_scoreViewPrefab).AsSingle();
            Container.Bind<PlayerStateView>().FromComponentInNewPrefab(_playerStateViewPrefab).AsSingle();
            Container.Bind<LaserView>().FromComponentInNewPrefab(_laserViewPrefab).AsSingle();
        }
    }
}