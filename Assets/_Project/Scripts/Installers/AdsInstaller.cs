using _Project.Scripts.Meta.Monetization;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class AdsInstaller : MonoInstaller
    {
        [SerializeField] private AdsConfig _adsConfig;

        public override void InstallBindings()
        {
            Container.Bind<AdsConfig>().FromScriptableObject(_adsConfig).AsSingle();
            Container.BindFactory<string, UnityAdHandler, UnityAdHandlerFactory>();
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS
            Container.Bind<IAdsService>().To<UnityAdsService>().AsSingle();
#elif UNITY_STANDALONE_WIN
            Container.Bind<IAdsService>().To<WindowsAdsService>().AsSingle();
#endif
        }
    }
}