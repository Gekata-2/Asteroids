using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Meta.IAP;
using _Project.Scripts.Meta.Monetization;
using _Project.Scripts.Services;
using _Project.Scripts.Services.AssetsManagement;
using _Project.Scripts.Services.DataPersistence;
using _Project.Scripts.Services.DataPersistence.SaveResolving;
using _Project.Scripts.Services.Network;
using _Project.Scripts.Sfx;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace _Project.Scripts.MainMenu
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
        private AudioSystem _audioSystem;
        private IAssetProvider _assetProvider;
        private AssetBundleConfig _assetBundleConfig;

        [Inject]
        private void Construct(
            IAdsService adsService,
            IAPModel iapModel,
            SaveResolvePresenter saveResolvePresenter,
            SaveLoadService saveLoadService,
            INetworkConnectionService networkConnectionService,
            CursorService cursorService,
            AudioSystem audioSystem,
            AssetBundleConfig assetBundleConfig,
            IAssetProvider assetProvider)
        {
            _adsService = adsService;
            _iapModel = iapModel;
            _saveResolvePresenter = saveResolvePresenter;
            _saveLoadService = saveLoadService;
            _connectionService = networkConnectionService;
            _cursorService = cursorService;
            _audioSystem = audioSystem;
            _assetProvider = assetProvider;
            _assetBundleConfig = assetBundleConfig;
        }

        private void Start()
        {
            Initialize().Forget();
        }

        private async UniTask Initialize()
        {
            _blockingBackground.SetActive(true);

            await UniTask.WhenAll(DownloadAssets(), LoadBanner());
            await LoadSave();

            if (!_audioSystem.Initialized)
                _audioSystem.Initialize();

            _blockingBackground.SetActive(false);
            _cursorService.SetCursorVisibility(true);
            _audioSystem.PlayMusic(SFX.MainMenuOst);
        }

        private async UniTask DownloadAssets()
        {
            await _assetProvider.Preload(_assetBundleConfig.UsedAssets.Select(AssetsNames.GetName).ToArray());
            await _assetProvider.Preload(_assetBundleConfig.UsedSfx.Select(AssetsNames.GetName).ToArray());
            await _assetProvider.Preload(_assetBundleConfig.UsedVfx.Select(AssetsNames.GetName).ToArray());
        }

        private async Task LoadSave()
        {
            SavesData savesData = await _saveLoadService.GetSavesData();

            if (savesData != null && savesData.IsSyncingRequired && _connectionService.IsOnline)
                _saveResolvePresenter.ResolveSaveSyncing(savesData);
            else
                _iapModel.FetchPurchasedProducts();
        }

        private async UniTask LoadBanner()
        {
            _adsService.LoadBanner(_bannerPosition);
            await UniTask.WaitUntil(() => _adsService.IsBannerLoaded);
            _adsService.ShowBanner();
        }
    }
}