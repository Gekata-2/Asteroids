using _Project.Scripts.Level.GameSession;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.DataPersistence
{
    public class SaveProvider
    {
        private readonly GameSessionData _sessionData;
        private readonly ISaveLoadService _saveLoadService;

        public SaveProvider(GameSessionData sessionData, ISaveLoadService saveLoadService)
        {
            _sessionData = sessionData;
            _saveLoadService = saveLoadService;
        }

        public async UniTask<SaveData> CreateSave()
        {
            SaveData saveData = await _saveLoadService.Load();
            return new SaveData(_sessionData.Score, _sessionData.TimeElapsed, saveData.IsAdsRemoved);
        }
    }
}