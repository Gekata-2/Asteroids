using System;
using _Project.Scripts.DataPersistence;
using _Project.Scripts.Services.Monetization;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.Services.IAP
{
    public class IAPModel : IInitializable, IDisposable
    {
        public event Action<IAP> IAPPurchaseSuccess;
        public event Action<IAP> IAPPurchaseFailed;
        public event Action<IAP> IAPEntitled;

        private readonly IIAPService _iapService;
        private readonly IapConfig _iapConfig;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IAdsService _adsService;

        public IAPModel(IapConfig iapConfig, IIAPService iapService,
            ISaveLoadService saveLoadService,
            IAdsService adsService)
        {
            _iapService = iapService;
            _saveLoadService = saveLoadService;
            _adsService = adsService;
            _iapConfig = iapConfig;
        }

        public void Initialize()
        {
            _iapService.PurchaseSuccess += OnPurchaseSuccess;
            _iapService.PurchaseFailed += OnPurchaseFailed;
        }

        private void OnPurchaseFailed(string id)
        {
            IAPPurchaseFailed?.Invoke(_iapConfig.GetIAPByName(id));
        }

        private void OnPurchaseSuccess(string id)
        {
            IAP iap = _iapConfig.GetIAPByName(id);
            if (iap == IAP.RemoveAds)
                SaveAdsRemoved().Forget();

            IAPPurchaseSuccess?.Invoke(iap);
        }

        private async UniTask SaveAdsRemoved()
        {
            SaveData saveData = await _saveLoadService.Load();
            saveData.IsAdsRemoved = true;
            _adsService.SetEnabled(!saveData.IsAdsRemoved);
            await _saveLoadService.Save(saveData);
        }

        public async UniTask FetchPurchasedProducts()
        {
            SaveData saveData = await _saveLoadService.Load();

            if (saveData.IsAdsRemoved)
                IAPEntitled?.Invoke(IAP.RemoveAds);

            _iapService.FetchPurchases();
        }

        public void Purchase(IAP iap)
        {
            _iapService.Purchase(_iapConfig.GetStoreName(iap));
        }

        public void Dispose()
        {
            _iapService.PurchaseSuccess -= OnPurchaseSuccess;
            _iapService.PurchaseFailed -= OnPurchaseFailed;
        }
    }
}