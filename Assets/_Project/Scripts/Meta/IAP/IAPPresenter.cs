using System;
using _Project.Scripts.Meta.IAP.UI;
using Zenject;

namespace _Project.Scripts.Meta.IAP
{
    public class IAPPresenter : IInitializable, IDisposable
    {
        private readonly IAPWindow _view;
        private readonly IAPModel _model;
        private readonly IapConfig _iapConfig;

        public IAPPresenter(IapConfig iapConfig, IAPModel model, IAPWindow view)
        {
            _model = model;
            _view = view;
            _iapConfig = iapConfig;
        }

        public void Initialize()
        {
            _view.BuyClicked += OnBuyClicked;
            _model.IAPPurchaseSuccess += OnIAPPurchaseSuccess;
            _model.IAPPurchaseFailed += OnIAPPurchaseFailed;
            _model.IAPEntitled += OnIAPEntitled;
        }

        private void OnIAPPurchaseFailed(IAP iap)
        {
            _view.SetAllIsProcessing(false);
        }

        private void OnIAPPurchaseSuccess(IAP iap)
        {
            _view.SetAllIsProcessing(false);
            if (CanBePurchasedOnce(iap))
                _view.SetIsAvailable(iap, false);
        }

        private void OnIAPEntitled(IAP iap)
        {
            if (CanBePurchasedOnce(iap))
                _view.SetIsAvailable(iap, false);
        }

        private bool CanBePurchasedOnce(IAP iap)
            => _iapConfig.GetIAPData(iap).Type is IAPType.NonConsumable or IAPType.Subscription;


        private void OnBuyClicked(IAP iap)
        {
            _view.SetAllIsProcessing(true);
            _model.Purchase(iap);
        }

        public void Dispose()
        {
            _view.BuyClicked -= OnBuyClicked;
            _model.IAPPurchaseSuccess -= OnIAPPurchaseSuccess;
            _model.IAPPurchaseFailed -= OnIAPPurchaseFailed;
            _model.IAPEntitled -= OnIAPEntitled;
        }
    }
}