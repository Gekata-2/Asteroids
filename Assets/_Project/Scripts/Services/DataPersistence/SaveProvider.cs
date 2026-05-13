using System;
using _Project.Scripts.Level;

namespace _Project.Scripts.Services.DataPersistence
{
    public class SaveProvider
    {
        private readonly GameSessionData _sessionData;
        private readonly SaveLoadService _saveLoadService;

        public SaveProvider(GameSessionData sessionData, SaveLoadService saveLoadService)
        {
            _sessionData = sessionData;
            _saveLoadService = saveLoadService;
        }

        public SaveData CreateSave()
        {
            SaveData saveData = _saveLoadService.CurrentSave;
            return new SaveData(_sessionData.Score, _sessionData.TimeElapsed, DateTime.Now, saveData.IsAdsRemoved);
        }
    }
}