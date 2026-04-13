using _Project.Scripts.Analytics;
using _Project.Scripts.DataPersistence;
using _Project.Scripts.Services.Logging;
using _Project.Scripts.Services.SceneManagement;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnityConsoleLogger>().AsSingle()
                .WithArguments(LogModule.All).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerPrefsSaveLoadService>().AsSingle();
            Container.BindInterfacesAndSelfTo<FirebaseAnalyticsService>().AsSingle();
        }
    }
}