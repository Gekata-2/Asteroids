using System;
using _Project.Scripts.Services.Logging;
using UnityEngine.Advertisements;

namespace _Project.Scripts.Services.Monetization
{
    public class UnityAdHandler : IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private readonly ILogger _logger;
        private readonly string _unitId;

        private Action _onAdShowComplete;
        public bool IsAdLoaded { get; private set; }
        public bool IsShowing { get; private set; }

        public UnityAdHandler(string unitId, ILogger logger)
        {
            _logger = logger;
            _unitId = unitId;
        }

        public void LoadAd()
        {
            IsAdLoaded = false;
            Advertisement.Load(_unitId, this);
        }

        public void ShowAd(Action onCompleted = null)
        {
            if (!IsAdLoaded)
            {
                LoadAd();
                return;
            }

            IsShowing = false;
            _onAdShowComplete = onCompleted;

            Advertisement.Show(_unitId, this);
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            if (placementId != _unitId)
                return;

            _logger.LogAds($"Ads: {placementId} loaded");
            IsAdLoaded = true;
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            IsAdLoaded = false;
            _logger.LogWarning($"Error loading Ad Unit: {_unitId} - {error} - {message}");
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            _logger.LogAds($"{placementId} Ad completed");

            IsAdLoaded = false;
            IsShowing = false;

            LoadAd();

            _onAdShowComplete?.Invoke();
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            IsShowing = false;
            _logger.LogWarning($"Error showing Ad Unit {_unitId}: {error} - {message}");
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            IsShowing = true;
            _logger.LogAds($"{placementId} Ad show start");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            _logger.LogAds($"{placementId} Ad click");
        }
    }
}