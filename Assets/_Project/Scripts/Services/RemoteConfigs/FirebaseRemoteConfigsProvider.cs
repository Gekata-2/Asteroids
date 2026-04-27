using System;
using Cysharp.Threading.Tasks;
using Firebase.RemoteConfig;
using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Scripts.Services.RemoteConfigs
{
    public class FirebaseRemoteConfigsProvider : IConfigsProvider
    {
        public async UniTask FetchData()
        {
            await FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero).AsUniTask();
            FirebaseRemoteConfig remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            ConfigInfo info = remoteConfig.Info;
            if (info.LastFetchStatus != LastFetchStatus.Success)
                Debug.LogError($"Fetch Remote configs was unsuccessful");
            else if (info.LastFetchStatus == LastFetchStatus.Success)
                Debug.Log($"Fetched remote configs from firebase");
        }

        public async UniTask ActivateData()
        {
            FirebaseRemoteConfig remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            await remoteConfig.ActivateAsync();
        }

        public T GetValue<T>(string key)
        {
            string json = FirebaseRemoteConfig.DefaultInstance.GetValue(key).StringValue;
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}