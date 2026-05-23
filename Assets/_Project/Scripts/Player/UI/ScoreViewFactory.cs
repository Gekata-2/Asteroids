using _Project.Scripts.Services.AssetsManagement;
using UnityEngine;

namespace _Project.Scripts.Player.UI
{
    public class ScoreViewFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ScoreViewPrefabFactory _prefabFactory;

        public ScoreViewFactory(IAssetProvider assetProvider, ScoreViewPrefabFactory prefabFactory)
        {
            _assetProvider = assetProvider;
            _prefabFactory = prefabFactory;
        }

        public ScoreView Create()
        {
            _assetProvider.TryGetAsset(AssetsNames.GetName(Asset.ScoreUI), out Object prefab);
            return _prefabFactory.Create(prefab);
        }
    }
}