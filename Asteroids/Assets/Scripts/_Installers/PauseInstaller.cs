using Services;
using Zenject;

namespace _Installers
{
    public class PauseInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PauseService>().FromNew().AsSingle().NonLazy();
        }
    }
}