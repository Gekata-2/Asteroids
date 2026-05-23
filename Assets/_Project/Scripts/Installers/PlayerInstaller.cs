using _Project.Scripts.Player;
using _Project.Scripts.Player.UI;
using _Project.Scripts.Player.Weapons.Laser;
using _Project.Scripts.Player.Weapons.Laser.UI;
using _Project.Scripts.Player.Weapons.MachineGun;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindFactory<Object, Player.Player, PlayerPrefabFactory>().FromFactory<PrefabFactory<Player.Player>>();
            Container.BindInterfacesAndSelfTo<PlayerFactory>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<PlayerStatePresenter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerScorePresenter>().AsSingle();

            Container.BindInterfacesAndSelfTo<MachineGunModel>().AsSingle();

            Container.BindInterfacesAndSelfTo<LaserModel>().AsSingle();
            Container.BindInterfacesTo<LaserPresenter>().AsSingle();
            
            Container.BindFactory<Object, ScoreView, ScoreViewPrefabFactory>().FromFactory<PrefabFactory<ScoreView>>();
            Container.Bind<ScoreViewFactory>().AsSingle();
            Container.BindFactory<Object, PlayerStateView, PlayerStateViewPrefabFactory>().FromFactory<PrefabFactory<PlayerStateView>>();
            Container.Bind<PlayerStateViewFactory>().AsSingle();
            Container.BindFactory<Object, LaserView, LaserViewPrefabFactory>().FromFactory<PrefabFactory<LaserView>>();
            Container.Bind<LaserViewFactory>().AsSingle();
          
        }
    }
}