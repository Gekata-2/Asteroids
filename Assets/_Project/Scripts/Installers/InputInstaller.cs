using _Project.Scripts.Player;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();
            Container.Bind<PlayerInputActionMap>().AsSingle();
        }
    }
}