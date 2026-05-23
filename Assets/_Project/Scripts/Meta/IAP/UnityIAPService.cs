using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine.Purchasing;
using ILogger = _Project.Scripts.Services.Logging.ILogger;

namespace _Project.Scripts.Meta.IAP
{
    public class UnityIAPService : IIAPService, IDisposable
    {
        public event Action<string> PurchaseSuccess;
        public event Action<string> PurchaseFailed;
        public event Action<string> PurchasedProductFetched;

        private readonly IapConfig _iapConfig;
        private readonly ILogger _logger;
        private StoreController _storeController;

        public UnityIAPService(IapConfig iapConfig, ILogger logger)
        {
            _iapConfig = iapConfig;
            _logger = logger;
        }

        public async UniTask Initialize()
        {
            CatalogProvider catalogProvider = new CatalogProvider();
            catalogProvider.AddProduct(_iapConfig.GetStoreName(IAP.RemoveAds), ProductType.NonConsumable);

            _storeController = UnityIAPServices.StoreController();
            
            _storeController.OnStoreConnected += OnStoreConnected;
            _storeController.OnStoreDisconnected += OnStoreDisconnected;

            await _storeController.Connect().AsUniTask();

            _storeController.OnProductsFetched += OnProductsFetched;
            _storeController.OnProductsFetchFailed += OnProductsFetchFailed;

            _storeController.OnPurchasesFetched += OnPurchasesFetched;
            _storeController.OnPurchasesFetchFailed += OnPurchasesFetchFailed;

            _storeController.OnPurchaseConfirmed += OnPurchaseConfirmed;
            _storeController.OnPurchaseDeferred += OnPurchaseDeferred;
            _storeController.OnPurchaseFailed += OnPurchaseFailed;
            _storeController.OnPurchasePending += OnPurchasePending;

            catalogProvider.FetchProducts(list => _storeController.FetchProducts(list));
        }


        private void OnStoreConnected()
        {
            _logger.LogIAP("Store connected");
        }

        private void OnStoreDisconnected(StoreConnectionFailureDescription failure)
        {
            _logger.LogIAP("Store disconnected");
        }

        private void OnProductsFetched(List<Product> products)
        {
            _storeController.FetchPurchases();
        }

        private void OnProductsFetchFailed(ProductFetchFailed failure)
        {
            _logger.LogIAP("Products fetch failed");
        }

        private void OnPurchasesFetched(Orders orders)
        {
            foreach (var confirmedOrder in orders.ConfirmedOrders)
            {
                Product product = confirmedOrder.CartOrdered.Items().FirstOrDefault()?.Product;
                if (product == null)
                    continue;
                if (product.definition.type != ProductType.Consumable)
                    PurchasedProductFetched?.Invoke(product.definition.id);
            }
        }

        private void OnPurchasesFetchFailed(PurchasesFetchFailureDescription failure)
        {
            _logger.LogIAP("Purchases fetch failed");
        }

        private void OnPurchasePending(PendingOrder order)
        {
            _storeController.ConfirmPurchase(order);
        }

        private void OnPurchaseConfirmed(Order order)
        {
            foreach (CartItem item in order.CartOrdered.Items())
                PurchaseSuccess?.Invoke(item.Product.definition.id);
        }

        private void OnPurchaseDeferred(DeferredOrder order)
        {
            _logger.LogIAP("Purchases deferred");
        }

        private void OnPurchaseFailed(FailedOrder order)
        {
            foreach (CartItem item in order.CartOrdered.Items())
                PurchaseFailed?.Invoke(item.Product.definition.id);
        }

        public void Purchase(string productId)
            => _storeController?.PurchaseProduct(productId);

        public void FetchPurchases()
            => _storeController.FetchPurchases();

        public void Dispose()
        {
            _storeController.OnStoreConnected -= OnStoreConnected;
            _storeController.OnStoreDisconnected -= OnStoreDisconnected;
            
            _storeController.OnProductsFetched -= OnProductsFetched;
            _storeController.OnProductsFetchFailed -= OnProductsFetchFailed;

            _storeController.OnPurchasesFetched -= OnPurchasesFetched;
            _storeController.OnPurchasesFetchFailed -= OnPurchasesFetchFailed;

            _storeController.OnPurchaseConfirmed -= OnPurchaseConfirmed;
            _storeController.OnPurchaseDeferred -= OnPurchaseDeferred;
            _storeController.OnPurchaseFailed -= OnPurchaseFailed;
            _storeController.OnPurchasePending -= OnPurchasePending;
        }
    }
}