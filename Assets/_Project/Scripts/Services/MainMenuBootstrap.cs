using System.Threading.Tasks;
using _Project.Scripts.Services.DataPersistence;
using _Project.Scripts.Services.IAP;
using _Project.Scripts.Services.Monetization;
using _Project.Scripts.Services.Network;
using _Project.Scripts.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace _Project.Scripts.Services
{
    public class MainMenuBootstrap : MonoBehaviour
    {
        [SerializeField] private BannerPosition _bannerPosition;
        [SerializeField] private GameObject _blockingBackground;

        private IAdsService _adsService;
        private IAPModel _iapModel;
        private SaveLoadService _saveLoadService;
        private SaveResolvePresenter _saveResolvePresenter;
        private INetworkConnectionService _connectionService;
        private CursorService _cursorService;

        [Inject]
        private void Construct(IAdsService adsService, IAPModel iapModel, SaveLoadService saveLoadService,
            SaveResolvePresenter saveResolvePresenter, INetworkConnectionService networkConnectionService,
            CursorService cursorService)
        {
            _adsService = adsService;
            _iapModel = iapModel;
            _saveResolvePresenter = saveResolvePresenter;
            _saveLoadService = saveLoadService;
            _connectionService = networkConnectionService;
            _cursorService = cursorService;
        }

        private void Start()
        {
            Initialize().Forget();
        }

        private async UniTask Initialize()
        {
            _blockingBackground.SetActive(true);
            
            await LoadBanner();
            await LoadSave();

            _blockingBackground.SetActive(false);
            _cursorService.SetCursorVisibility(true);
        }

        private async Task LoadSave()
        {
            SavesData savesData = await _saveLoadService.GetSavesData();

            if (savesData != null && savesData.IsSyncingRequired && _connectionService.IsOnline)
                _saveResolvePresenter.ResolveSaveSyncing(savesData);
            else
                _iapModel.FetchPurchasedProducts();
        }

        private async Task LoadBanner()
        {
            _adsService.LoadBanner(_bannerPosition);
            await UniTask.WaitUntil(() => _adsService.IsBannerLoaded);
            _adsService.ShowBanner();
        }
    }
}