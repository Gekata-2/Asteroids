using System;
using _Project.Scripts.Services.DataPersistence;
using UnityEngine;

namespace _Project.Scripts.UI.Windows
{
    public class SaveResolveWindow : MonoBehaviour
    {
        public event Action LocalSaveSelected;
        public event Action CloudSaveSelected;

        [SerializeField] private SaveView _localSave;
        [SerializeField] private SaveView _cloudSave;

        private void Start()
        {
            _localSave.Clicked += OnLocalSaveClicked;
            _cloudSave.Clicked += OnCloudSaveClicked;
        }

        private void OnDestroy()
        {
            _localSave.Clicked -= OnLocalSaveClicked;
            _cloudSave.Clicked -= OnCloudSaveClicked;
        }

        private void OnCloudSaveClicked()
            => CloudSaveSelected?.Invoke();

        private void OnLocalSaveClicked()
            => LocalSaveSelected?.Invoke();

        public void Show(SaveData localSave, SaveData cloudSave)
        {
            _localSave.Initialize(localSave);
            _cloudSave.Initialize(cloudSave);
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}