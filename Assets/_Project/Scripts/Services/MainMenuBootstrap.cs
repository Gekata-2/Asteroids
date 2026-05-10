using _Project.Scripts.Services.IAP;
using _Project.Scripts.Services.Monetization;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace _Project.Scripts.Services
{
    public class MainMenuBootstrap : MonoBehaviour
    {
        [SerializeField] private BannerPosition _bannerPosition;

        private IAdsService _adsService;
        private IAPModel _iapModel;

        [Inject]
        private void Construct(IAdsService adsService, IAPModel iapModel)
        {
            _adsService = adsService;
            _iapModel = iapModel;
        }

        private void Start()
        {
            ShowBanner().Forget();
        }

        private async UniTask ShowBanner()
        {
            _adsService.LoadBanner(_bannerPosition);
            await UniTask.WaitUntil(() => _adsService.IsBannerLoaded);
            await _iapModel.FetchPurchasedProducts();
            _adsService.ShowBanner();
        }
    }
}