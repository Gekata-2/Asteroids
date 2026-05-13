using System;
using System.Collections.Generic;
using _Project.Scripts.Services.Logging;
using Cysharp.Threading.Tasks;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;

namespace _Project.Scripts.Services.DataPersistence.Cloud
{
    public class UnityCloudSaveService : ICloudSaveLoadService
    {
        private const string SAVE_NAME = "save";
        private readonly ILogger _logger;

        public UnityCloudSaveService(ILogger logger)
        {
            _logger = logger;
        }

        public async UniTask<SaveData> Load()
        {
            try
            {
                Dictionary<string, Item> playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(
                    new HashSet<string> { SAVE_NAME }).AsUniTask();
                if (playerData.TryGetValue(SAVE_NAME, out Item item))
                {
                    SaveData data = item.Value.GetAs<SaveData>();
                    _logger.LogSave($"Loaded from Cloud: {data}");
                    return data;
                }
            }
            catch (Exception e)
            {
                _logger.LogException(e);
            }

            return await UniTask.FromResult<SaveData>(null);
        }


        public async UniTask Save(SaveData data)
        {
            Dictionary<string, object> playerData = new Dictionary<string, object> { { SAVE_NAME, data } };
            try
            {
                await CloudSaveService.Instance.Data.Player.SaveAsync(playerData);
                _logger.LogSave($"Saved to Cloud: {data}");
            }
            catch (Exception e)
            {
                _logger.LogException(e);
            }
        }
    }
}