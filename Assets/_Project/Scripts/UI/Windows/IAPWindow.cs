using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Services.IAP;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI.Windows
{
    public class IAPWindow : MonoBehaviour
    {
        public event Action<IAP> BuyClicked;

        [SerializeField] private List<ProductCardView> _products = new();

        private IapConfig _iapConfig;

        [Inject]
        private void Construct(IapConfig iapConfig)
        {
            _iapConfig = iapConfig;
        }

        private void OnDestroy()
        {
            UnsubscribeFromViews();
        }

        private void Start()
        {
            foreach (ProductCardView view in _products)
            {
                view.Initialize(view.IAP, _iapConfig.GetIAPData(view.IAP));
                SubscribeToView(view);
            }
        }

        private void SubscribeToView(ProductCardView view)
        {
            view.BuyClicked += OnBuyClicked;
        }

        private void UnsubscribeFromViews()
        {
            foreach (ProductCardView view in _products)
                view.BuyClicked -= OnBuyClicked;
        }

        private void OnBuyClicked(ProductCardView view)
        {
            BuyClicked?.Invoke(view.IAP);
        }

        public void SetAllIsProcessing(bool isProcessing)
        {
            foreach (ProductCardView view in _products)
                view.SetIsProcessing(isProcessing);
        }

        public void SetIsAvailable(IAP iap, bool isActive)
        {
            ProductCardView view = GetView(iap);
            view?.SetAvailable(isActive);
        }

        private ProductCardView GetView(IAP iap)
            => _products.FirstOrDefault(v => v.IAP == iap);
    }
}