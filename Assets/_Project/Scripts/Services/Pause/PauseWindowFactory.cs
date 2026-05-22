using _Project.Scripts.Services.AssetsManagement;
using UnityEngine;

namespace _Project.Scripts.Services.Pause
{
    public class PauseWindowFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly PauseWindowPrefabFactory _prefabFactory;
        
        public PauseWindowFactory(IAssetProvider assetProvider, PauseWindowPrefabFactory prefabFactory)
        {
            _assetProvider = assetProvider;
            _prefabFactory = prefabFactory;
        }
        
        public PauseWindow Create()
        {
            _assetProvider.TryGetAsset(AssetsNames.GetName(Asset.PauseUI), out Object prefab);
            return _prefabFactory.Create(prefab);
        }
    }
}