using _Project.Scripts.Services;
using _Project.Scripts.Services.Awards;
using _Project.Scripts.Services.EventBus;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PauseService>().FromNew().AsSingle();
            Container.Bind<EventBus>().FromNew().AsSingle();
            Container.Bind<UIManager>().FromNew().AsSingle();
            Container.Bind<CursorService>().FromNew().AsSingle();
            Container.Bind<ExitGameService>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<TimeService>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<AwardsController>().FromNew().AsSingle().NonLazy();
        }
    }
}