using System;
using _Project.Scripts.Services.Logging;
using UnityEngine.Advertisements;

namespace _Project.Scripts.Services.Monetization
{
    public class UnityAdsService : IAdsService, IUnityAdsInitializationListener
    {
        private readonly AdsConfig _adsConfig;
        private readonly ILogger _logger;
        private readonly UnityAdHandlerFactory _adHandlerFactory;

        private UnityAdHandler _interstitialAdsHandler;
        private UnityAdHandler _rewardedAdsHandler;

        private string _gameId;
        private string _bannerId;

        public bool IsBannerLoaded { get; private set; }
        public bool IsBannerShown { get; private set; }

        public bool IsInitialized
            => Advertisement.isInitialized;

        public bool IsInterstitialAdReady
            => _interstitialAdsHandler.IsAdLoaded;

        public bool IsRewardedAdReady
            => _rewardedAdsHandler.IsAdLoaded;

        public bool IsShowingAd
            => _rewardedAdsHandler.IsShowing || _interstitialAdsHandler.IsShowing;

        public UnityAdsService(AdsConfig adsConfig, ILogger logger, UnityAdHandlerFactory adHandlerFactory)
        {
            _adsConfig = adsConfig;
            _logger = logger;
            _adHandlerFactory = adHandlerFactory;
        }

        public void Initialize()
        {
            if (Advertisement.isInitialized || !Advertisement.isSupported)
                return;
#if UNITY_ANDROID
            InitializeIds(_adsConfig.AndroidGameId, _adsConfig.InterstitialAndroidAdUnitId,
                _adsConfig.RewardedAndroidAdUnitId, _adsConfig.BannerAndroidAdUnitId);
#elif UNITY_IOS
            InitializeIds(_adsConfig.IOSGameId, _adsConfig.InterstitialIOSAdUnitId, _adsConfig.RewardedIOSAdUnitId,
                _adsConfig.BannerIOSAdUnitId);
#elif UNITY_EDITOR
            InitializeIds(_adsConfig.AndroidGameId, _adsConfig.InterstitialAndroidAdUnitId,
                _adsConfig.RewardedAndroidAdUnitId, _adsConfig.BannerAndroidAdUnitId);
#endif
            Advertisement.Initialize(_gameId, _adsConfig.TestMode, this);
        }

        private void InitializeIds(string gameId, string interstitialAdId, string rewardedAdId, string bannerId)
        {
            _gameId = gameId;
            _rewardedAdsHandler = _adHandlerFactory.Create(rewardedAdId);
            _interstitialAdsHandler = _adHandlerFactory.Create(interstitialAdId);
            _bannerId = bannerId;
        }

        public void OnInitializationComplete()
            => _logger.LogAds("Ads: Initialization Completed");

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
            => _logger.LogException(new Exception($"Ads: error {error}, {message}"));

        public void LoadInterstitialAd()
            => _interstitialAdsHandler.LoadAd();

        public void ShowInterstitialAd(Action onCompleted = null)
            => _interstitialAdsHandler.ShowAd(onCompleted);

        public void LoadRewardedAd()
            => _rewardedAdsHandler.LoadAd();

        public void ShowRewardedAd(Action onCompleted = null)
            => _rewardedAdsHandler.ShowAd(onCompleted);

        public void LoadBanner(BannerPosition position)
        {
            Advertisement.Banner.SetPosition(position);

            BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerLoadError
            };
            IsBannerLoaded = false;
            Advertisement.Banner.Load(_bannerId, options);
        }

        private void OnBannerLoadError(string message)
            => _logger.LogWarning($"Ads: {message}");

        private void OnBannerLoaded()
            => IsBannerLoaded = true;

        public void ShowBanner()
        {
            BannerOptions options = new BannerOptions
            {
                clickCallback = OnBannerClicked,
                hideCallback = OnBannerHidden,
                showCallback = OnBannerShown
            };
            Advertisement.Banner.Show(_bannerId, options);
        }

        public void HideBanner()
            => Advertisement.Banner.Hide();

        private void OnBannerShown()
            => IsBannerShown = true;

        private void OnBannerHidden()
            => IsBannerShown = false;

        private void OnBannerClicked()
        {
        }
    }
}