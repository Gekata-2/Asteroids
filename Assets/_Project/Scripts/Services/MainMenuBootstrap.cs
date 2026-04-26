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

        [Inject]
        private void Construct(IAdsService adsService)
        {
            _adsService = adsService;
        }

        private void Start()
        {
            ShowBanner().Forget();
        }

        private async UniTask ShowBanner()
        {
            _adsService.LoadBanner(_bannerPosition);
            await UniTask.WaitUntil(() => _adsService.IsBannerLoaded);
            _adsService.ShowBanner();
        }
    }
}