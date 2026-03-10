using Services;
using Zenject;

namespace _Installers
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PauseService>().FromNew().AsSingle().NonLazy();
            Container.Bind<EventBus>().FromNew().AsSingle().NonLazy();
            Container.Bind<UIManager>().FromNew().AsSingle().NonLazy();
            Container.Bind<ExitGameService>().FromNew().AsSingle().NonLazy();
            Container.Bind<CursorService>().FromNew().AsSingle().NonLazy();
        }
    }
}