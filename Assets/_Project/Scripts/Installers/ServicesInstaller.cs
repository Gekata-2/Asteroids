using _Project.Scripts.DataPersistence;
using _Project.Scripts.Services;
using _Project.Scripts.Services.Analytics;
using _Project.Scripts.Services.AssetsProviding;
using _Project.Scripts.Services.Awards;
using _Project.Scripts.Services.Pause;
using _Project.Scripts.Services.UI;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PauseService>().FromNew().AsSingle();
            Container.Bind<UIManager>().FromNew().AsSingle();
            Container.Bind<CursorService>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<TimeService>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<AwardsController>().FromNew().AsSingle().NonLazy();

            Container.Bind<AnalyticsDataBuilder>().AsSingle();
            Container.Bind<SaveProvider>().AsSingle();

            Container.Bind<AssetsNames>().AsSingle();
            Container.BindInterfacesAndSelfTo<AddressablesProvider>().AsSingle();
        }
    }
}