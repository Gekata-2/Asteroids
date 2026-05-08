using System;
using _Project.Scripts.Services.DataPersistence;
using _Project.Scripts.Services.IAP;
using _Project.Scripts.UI.Windows;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.UI
{
    public class SaveResolvePresenter : IInitializable, IDisposable
    {
        private readonly SaveResolveWindow _view;
        private readonly SaveLoadService _saveLoadService;
        private readonly IAPModel _iapModel;

        private SavesData _savesData;

        public SaveResolvePresenter(SaveResolveWindow view, SaveLoadService saveLoadService, IAPModel iapModel)
        {
            _view = view;
            _saveLoadService = saveLoadService;
            _iapModel = iapModel;
        }

        public void Initialize()
        {
            _view.LocalSaveSelected += OnLocalSaveSelected;
            _view.CloudSaveSelected += OnCloudSaveSelected;
        }
        
        public void ResolveSaveSyncing(SavesData savesData)
        {
            _savesData = savesData;
            _view.Show(_savesData.LocalSave, _savesData.CloudSave);
        }

        private void OnLocalSaveSelected() 
            => HandleSaveResolved(_savesData.LocalSave).Forget();

        private void OnCloudSaveSelected() 
            => HandleSaveResolved(_savesData.CloudSave).Forget();

        private async UniTask HandleSaveResolved(SaveData saveData)
        {
            await _saveLoadService.Save(saveData);
            _iapModel.FetchPurchasedProducts();
            _view.Hide();
        }

        public void Dispose()
        {
            _view.LocalSaveSelected -= OnLocalSaveSelected;
            _view.CloudSaveSelected -= OnCloudSaveSelected;
        }
    }
}