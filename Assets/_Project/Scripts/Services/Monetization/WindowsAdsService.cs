using System;
using UnityEngine.Advertisements;

namespace _Project.Scripts.Services.Monetization
{
    public class WindowsAdsService : IAdsService
    {
        public bool IsInitialized { get; private set; }
        public bool IsRewardedAdReady { get; private set; }
        public bool IsInterstitialAdReady { get; private set; }
        public bool IsShowingAd => false;
        public bool IsBannerLoaded { get; private set; }
        public bool IsBannerShown { get; private set; }

        public void Initialize()
        {
            IsInitialized = true;
        }

        public void LoadInterstitialAd()
        {
            IsInterstitialAdReady = true;
        }

        public void ShowInterstitialAd(Action onCompleted = null)
        {
            IsInterstitialAdReady = false;
            onCompleted?.Invoke();
        }

        public void LoadRewardedAd()
        {
            IsRewardedAdReady = true;
        }

        public void ShowRewardedAd(Action onCompleted = null)
        {
            IsRewardedAdReady = false;
            onCompleted?.Invoke();
        }

        public void LoadBanner(BannerPosition position)
        {
            IsBannerLoaded = true;
        }

        public void ShowBanner()
        {
            IsBannerShown = true;
        }

        public void HideBanner()
        {
            IsBannerShown = false;
        }

        public void SetEnabled(bool isEnabled)
        {
        }
    }
}