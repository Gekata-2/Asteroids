using System;
using Cysharp.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace _Project.Scripts.Services.Authorization
{
    public class AnonymousAuthorizationService : IAuthorizationService
    {
        public async UniTask Initialize()
        {
            await UnityServices.InitializeAsync();
        }

        public async UniTask Authorize(string token)
        {
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}