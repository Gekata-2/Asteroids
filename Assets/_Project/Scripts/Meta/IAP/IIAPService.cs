using System;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Meta.IAP
{
    public interface IIAPService
    {
        event Action<string> PurchaseSuccess;
        event Action<string> PurchaseFailed;
        event Action<string> PurchasedProductFetched;
        UniTask Initialize();
        void Purchase(string productId);
        void FetchPurchases();
    }
}