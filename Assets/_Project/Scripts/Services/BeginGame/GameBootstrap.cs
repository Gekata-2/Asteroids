using System;
using _Project.Scripts.Meta.Analytics;
using _Project.Scripts.Meta.IAP;
using _Project.Scripts.Meta.Monetization;
using _Project.Scripts.Player;
using _Project.Scripts.Services.Authorization;
using _Project.Scripts.Services.DataPersistence;
using _Project.Scripts.Services.Network;
using _Project.Scripts.Services.RemoteConfigs;
using _Project.Scripts.Services.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services.BeginGame
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private bool _clearNonConsumablesPurchases;

        private SceneLoader _sceneLoader;
        private IIAPService _iapService;

        private IAnalytics _analytics;
        private IAdsService _adsService;
        private IConfigsProvider _configsProvider;
        private IAuthorizationService _authorizationService;
        private IInput _input;
        private INetworkConnectionService _connectionService;

        private SaveLoadService _saveService;

        [Inject]
        private void Construct(
            SceneLoader sceneLoader,
            IIAPService iapService,
            IAnalytics analytics,
            IAdsService adsService,
            IConfigsProvider configsProvider,
            IAuthorizationService authorizationService,
            IInput input,
            INetworkConnectionService networkConnectionService,
            SaveLoadService saveService)
        {
            _sceneLoader = sceneLoader;
            _iapService = iapService;

            _analytics = analytics;
            _adsService = adsService;
            _configsProvider = configsProvider;
            _authorizationService = authorizationService;
            _input = input;
            _connectionService = networkConnectionService;
            _saveService = saveService;
        }

        private void Start()
        {
            BootGame().Forget();
        }

        private async UniTask BootGame()
        {
            await LogIn();
            SaveData saveData = await RetrieveSave();

            await UniTask.WhenAll(
                _analytics.Initialize(),
                _configsProvider.FetchData(),
                _iapService.Initialize(),
                InitializeAdsService(!saveData.IsAdsRemoved),
                ClearNonConsumables(saveData));

            _input.EnableGlobalActions();
            _connectionService.Connect().Forget();
            _sceneLoader.LoadMainMenu();
        }

        private async UniTask LogIn()
        {
            await _authorizationService.Initialize();
            await _authorizationService.Authorize("test");
        }


        private async UniTask<SaveData> RetrieveSave()
        {
            SaveData save = await _saveService.Load();
            if (save != null)
                return save;

            save = CreateFreshSave();
            await _saveService.Save(save);

            return save;
        }

        private SaveData CreateFreshSave()
            => new(0, 0, DateTime.Now);

        private async UniTask InitializeAdsService(bool isAdsEnabled)
        {
            _adsService.Initialize();
            await UniTask.WaitUntil(() => _adsService.IsInitialized);

            _adsService.LoadInterstitialAd();
            _adsService.LoadRewardedAd();
            await UniTask.WaitUntil(() => _adsService.IsInterstitialAdReady && _adsService.IsRewardedAdReady);

            _adsService.SetEnabled(isAdsEnabled);
        }

        private async UniTask ClearNonConsumables(SaveData saveData)
        {
            if (_clearNonConsumablesPurchases)
            {
                saveData.IsAdsRemoved = false;
                await _saveService.Save(saveData);
            }
        }
    }
}