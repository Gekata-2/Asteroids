using UnityEngine;

namespace _Project.Scripts.Services.Monetization
{
   
    [CreateAssetMenu(menuName = "Scriptable Objects/Ads Config", fileName = "Ads Config", order = 0)]
    public class AdsConfig : ScriptableObject
    {
        [field:Header("Game")]
        [field: SerializeField] public string AndroidGameId { get; private set; }
        [field: SerializeField] public string IOSGameId { get; private set; }
        [field: SerializeField, Space] public bool TestMode { get; private set; }
   
        [field: Header("Interstitial")]
        [field: SerializeField] public string InterstitialAndroidAdUnitId { get; private set; }
        [field: SerializeField] public string InterstitialIOSAdUnitId { get; private set; }
      
        [field: Header("Rewarded")]
        [field: SerializeField] public string RewardedAndroidAdUnitId { get; private set; }
        [field: SerializeField] public string RewardedIOSAdUnitId { get; private set; }  
        
        [field: Header("Banner")]
        [field: SerializeField] public string BannerAndroidAdUnitId { get; private set; }
        [field: SerializeField] public string BannerIOSAdUnitId { get; private set; }
    }
}