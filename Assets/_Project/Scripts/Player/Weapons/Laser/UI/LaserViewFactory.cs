using _Project.Scripts.Services.AssetsManagement;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons.Laser.UI
{
    public class LaserViewFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly LaserViewPrefabFactory _prefabFactory;

        public LaserViewFactory(IAssetProvider assetProvider, LaserViewPrefabFactory prefabFactory)
        {
            _assetProvider = assetProvider;
            _prefabFactory = prefabFactory;
        }

        public LaserView Create()
        {
            _assetProvider.TryGetAsset(AssetsNames.GetName(Asset.LaserUI), out Object prefab);
            return _prefabFactory.Create(prefab);
        }
    }
}