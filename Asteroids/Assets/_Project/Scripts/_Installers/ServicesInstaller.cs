using _Project.Scripts.Services;
using _Project.Scripts.Services.Awards;
using _Project.Scripts.Services.EventBus;
using Zenject;

namespace _Project.Scripts._Installers
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
            Container.BindInterfacesAndSelfTo<TimeService>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AwardsController>().FromNew().AsSingle().NonLazy();
        }
    }
}