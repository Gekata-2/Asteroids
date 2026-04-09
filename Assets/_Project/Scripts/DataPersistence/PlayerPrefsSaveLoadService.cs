using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts.DataPersistence
{
    public class PlayerPrefsSaveLoadService : ISaveLoadService
    {
        private readonly string _key;
        private readonly bool _debug;

        public PlayerPrefsSaveLoadService(string key = "Save", bool debug = false)
        {
            _key = key;
            _debug = debug;
        }

        public UniTask<SaveData> Load()
        {
            string json = PlayerPrefs.GetString(_key, "");

            if (string.IsNullOrEmpty(json))
            {
                if (_debug)
                    Debug.Log("Save doesnt exists");
                return UniTask.FromResult<SaveData>(null);
            }

            SaveData data = JsonConvert.DeserializeObject<SaveData>(json);
            if (_debug)
                Debug.Log($"Loaded: {data}");
            return UniTask.FromResult(data);
        }

        public UniTask Save(SaveData data)
        {
            PlayerPrefs.SetString(_key, JsonConvert.SerializeObject(data, Formatting.Indented));
            PlayerPrefs.Save();
            if (_debug)
                Debug.Log($"Saved successfully: {data}");
            return UniTask.CompletedTask;
        }
    }
}