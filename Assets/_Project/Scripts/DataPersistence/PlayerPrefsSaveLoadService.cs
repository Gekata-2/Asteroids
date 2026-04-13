using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using ILogger = _Project.Scripts.Services.Logging.ILogger;

namespace _Project.Scripts.DataPersistence
{
    public class PlayerPrefsSaveLoadService : ISaveLoadService
    {
        private readonly string _key;
        private readonly ILogger _logger;

        public PlayerPrefsSaveLoadService(ILogger logger = null, string key = "Save")
        {
            _logger = logger;
            _key = key;
        }

        public UniTask<SaveData> Load()
        {
            string json = PlayerPrefs.GetString(_key, "");

            if (string.IsNullOrEmpty(json))
            {
                _logger?.LogSave("Save doesnt exists");
                return UniTask.FromResult<SaveData>(null);
            }

            SaveData data = JsonConvert.DeserializeObject<SaveData>(json);
            _logger?.LogSave($"Loaded: {data}");
            return UniTask.FromResult(data);
        }

        public UniTask Save(SaveData data)
        {
            PlayerPrefs.SetString(_key, JsonConvert.SerializeObject(data, Formatting.Indented));
            PlayerPrefs.Save();
            _logger?.LogSave($"Saved successfully: {data}");
            return UniTask.CompletedTask;
        }
    }
}