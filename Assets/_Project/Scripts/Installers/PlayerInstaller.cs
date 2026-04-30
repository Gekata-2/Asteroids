using _Project.Scripts.Level.GameSession;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons.Laser;
using _Project.Scripts.Player.Weapons.MachineGun;
using _Project.Scripts.UI;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerFactory>().AsSingle();

            Container.Bind<PlayerInputActionMap>().AsSingle();
            Container.Bind<IInput>().To<InputHandler>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerStatePresenter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerScorePresenter>().AsSingle();

            Container.BindInterfacesAndSelfTo<MachineGunModel>().AsSingle();

            Container.BindInterfacesAndSelfTo<LaserModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<LaserPresenter>().AsSingle();

            Container.Bind<AssetsFactory>().AsSingle();
        }
    }
}