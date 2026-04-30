using _Project.Scripts.DataPersistence;
using _Project.Scripts.Services;
using _Project.Scripts.Services.Analytics;
using _Project.Scripts.Services.Logging;
using _Project.Scripts.Services.Monetization;
using _Project.Scripts.Services.RemoteConfigs;
using _Project.Scripts.Services.SceneManagement;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private AdsConfig _adsConfig;

        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<ExitGameService>().FromNew().AsSingle();

            Container.BindInterfacesAndSelfTo<UnityConsoleLogger>().AsSingle()
                .WithArguments(LogModule.All).NonLazy();

            Container.BindInterfacesAndSelfTo<PlayerPrefsSaveLoadService>().AsSingle();

            Container.BindInterfacesAndSelfTo<FirebaseAnalyticsService>().AsSingle();

            Container.Bind<AdsConfig>().FromScriptableObject(_adsConfig).AsSingle();
            Container.BindFactory<string, UnityAdHandler, UnityAdHandlerFactory>();
            Container.BindInterfacesAndSelfTo<UnityAdsService>().AsSingle();

            Container.BindInterfacesAndSelfTo<FirebaseRemoteConfigsProvider>().AsSingle();
        }
    }
}