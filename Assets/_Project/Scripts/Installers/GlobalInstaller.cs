using _Project.Scripts.Player;
using _Project.Scripts.Services;
using _Project.Scripts.Services.Analytics;
using _Project.Scripts.Services.Authorization;
using _Project.Scripts.Services.DataPersistence;
using _Project.Scripts.Services.IAP;
using _Project.Scripts.Services.Logging;
using _Project.Scripts.Services.Monetization;
using _Project.Scripts.Services.Network;
using _Project.Scripts.Services.RemoteConfigs;
using _Project.Scripts.Services.SceneManagement;
using _Project.Scripts.UI;
using _Project.Scripts.UI.Windows;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private AdsConfig _adsConfig;
        [SerializeField] private IapConfig _iapConfig;
        [SerializeField] private NetworkWindow _networkWindowPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<NetworkWindow>().FromComponentInNewPrefab(_networkWindowPrefab)
                .AsSingle();
            Container.BindInterfacesAndSelfTo<NetworkPresenter>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<DummyNetworkConnectionService>().AsSingle();

            Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();
            Container.Bind<PlayerInputActionMap>().AsSingle();

            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<ExitGameService>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<AnonymousAuthorizationService>().AsSingle();

            Container.BindInterfacesAndSelfTo<UnityConsoleLogger>().AsSingle()
                .WithArguments(LogModule.All).NonLazy();


            Container.Bind<ILocalSaveLoadService>().To<PlayerPrefsSaveService>().AsCached();
            Container.Bind<ICloudSaveLoadService>().To<UnityCloudSaveService>().AsCached();
            Container.BindInterfacesAndSelfTo<SaveLoadService>().AsSingle();

            Container.BindInterfacesAndSelfTo<FirebaseAnalyticsService>().AsSingle();

            Container.Bind<AdsConfig>().FromScriptableObject(_adsConfig).AsSingle();
            Container.BindFactory<string, UnityAdHandler, UnityAdHandlerFactory>();
            Container.BindInterfacesAndSelfTo<UnityAdsService>().AsSingle();

            Container.BindInterfacesAndSelfTo<FirebaseRemoteConfigsProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnityIAPService>().AsSingle();
            Container.BindInterfacesAndSelfTo<IAPModel>().AsSingle();
            Container.Bind<IapConfig>().FromScriptableObject(_iapConfig).AsSingle();

            Container.Bind<CursorService>().FromNew().AsSingle();
        }
    }
}