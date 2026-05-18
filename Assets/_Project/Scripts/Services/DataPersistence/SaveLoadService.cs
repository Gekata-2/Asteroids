using System;
using _Project.Scripts.Services.DataPersistence.Cloud;
using _Project.Scripts.Services.DataPersistence.Local;
using _Project.Scripts.Services.Network;
using Cysharp.Threading.Tasks;
using ILogger = _Project.Scripts.Services.Logging.ILogger;

namespace _Project.Scripts.Services.DataPersistence
{
    public class SaveLoadService
    {
        private readonly ILocalSaveLoadService _localSaveService;
        private readonly ICloudSaveLoadService _cloudSaveService;
        private readonly INetworkConnectionService _connectionService;
        private readonly ILogger _logger;

        public SaveData CurrentSave { get; private set; }

        public SaveLoadService(
            ILocalSaveLoadService localSaveService,
            ICloudSaveLoadService cloudSaveService,
            INetworkConnectionService connectionService,
            ILogger logger)
        {
            _localSaveService = localSaveService;
            _cloudSaveService = cloudSaveService;
            _connectionService = connectionService;
            _logger = logger;
        }


        public async UniTask<SavesData> GetSavesData()
        {
            if (!_connectionService.IsOnline)
                return null;

            var (localSave, cloudSave) = await UniTask.WhenAll(_localSaveService.Load(), _cloudSaveService.Load());
            return new SavesData(localSave, cloudSave);
        }

        public async UniTask<SaveData> Load()
        {
            SaveData localSave = await _localSaveService.Load();

            if (!_connectionService.IsOnline)
            {
                CurrentSave = localSave;
                return localSave;
            }

            try
            {
                SaveData cloudSave = await _cloudSaveService.Load();
                CurrentSave = cloudSave;
                return cloudSave;
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message);

                CurrentSave = localSave;
                return localSave;
            }
        }

        public async UniTask Save(SaveData data)
        {
            await _localSaveService.Save(data);

            if (_connectionService.IsOnline)
            {
                try
                {
                    await _cloudSaveService.Save(data);
                }
                catch (Exception e)
                {
                    _logger.LogWarning(e.Message);
                }
            }

            CurrentSave = data;
        }
    }
}