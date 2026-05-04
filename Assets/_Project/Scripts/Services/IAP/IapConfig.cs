using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _Project.Scripts.Services.IAP
{
    [CreateAssetMenu(menuName = "Scriptable Objects/IAP Config", fileName = "IAP Config", order = 0)]
    public class IapConfig : ScriptableObject
    {
        [field: SerializeField] public SerializedDictionary<IAP, IAPData> Iaps { get; private set; }

        public string GetStoreName(IAP iap)
            => Iaps[iap].StoreName;

        public IAP GetIAPByName(string storeName)
            => Iaps.FirstOrDefault(i => i.Value.StoreName == storeName).Key;

        public IAPData GetIAPData(IAP iap)
            => Iaps[iap];
    }
}