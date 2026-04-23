using System;
using UnityEngine.Advertisements;

namespace _Project.Scripts.Services.Monetization
{
    public interface IAdsService
    {
        bool IsInitialized { get; }
      
        bool IsRewardedAdReady { get; }
        bool IsInterstitialAdReady { get; }
        bool IsShowingAd { get; }
        
        bool IsBannerLoaded { get; }
        bool IsBannerShown { get; }

        void Initialize();
        void LoadInterstitialAd();
        void ShowInterstitialAd(Action onCompleted = null);

        void LoadRewardedAd();
        void ShowRewardedAd(Action onCompleted = null);

        void LoadBanner(BannerPosition position);
        void ShowBanner();
        void HideBanner();
    }
}