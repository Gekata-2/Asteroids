using _Project.Scripts.Analytics;
using _Project.Scripts.DataPersistence;
using _Project.Scripts.Services.Monetization;
using _Project.Scripts.Services.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services.BeginGame
{
    public class GameBootstrap : MonoBehaviour
    {
        private SceneLoader _sceneLoader;
        private ISaveLoadService _saveLoadService;
        private IAnalytics _analytics;
        private IAdsService _adsService;


        [Inject]
        private void Construct(SceneLoader sceneLoader,
            ISaveLoadService saveLoadService,
            IAnalytics analytics,
            IAdsService adsService)
        {
            _sceneLoader = sceneLoader;
            _saveLoadService = saveLoadService;
            _analytics = analytics;
            _adsService = adsService;
        }

        private void Start()
        {
            BootGame().Forget();
        }

        private async UniTask BootGame()
        {
            await UniTask.WhenAll(_saveLoadService.Load(), _analytics.Initialize(), InitializeAdsService());

            _sceneLoader.LoadMainMenu();
        }

        private async UniTask InitializeAdsService()
        {
            _adsService.Initialize();
            await UniTask.WaitUntil(() => _adsService.IsInitialized);

            _adsService.LoadInterstitialAd();
            _adsService.LoadRewardedAd();
            await UniTask.WaitUntil(() => _adsService.IsInterstitialAdReady && _adsService.IsRewardedAdReady);
        }
    }
}