using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Extensions;
using UnityEngine;

namespace _Project.Scripts.Analytics
{
    public class FirebaseTest : MonoBehaviour
    {
        private void Start()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(OnDependencyStatusReceived);
        }

        private void OnDependencyStatusReceived(Task<DependencyStatus> task)
        {
            try
            {
                if (!task.IsCompletedSuccessfully)
                    Debug.LogException(new Exception("Couldn't resolve all Firebase dependencies", task.Exception));

                DependencyStatus status = task.Result;
                if (status != DependencyStatus.Available)
                    Debug.LogException(new Exception($"Couldn't resolve all Firebase dependencies: {status}"));

                Debug.Log("Firebase initialized successfully!!!");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}