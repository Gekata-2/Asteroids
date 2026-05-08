using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Services.Authorization
{
    public interface IAuthorizationService
    {
        UniTask Initialize();
        UniTask Authorize(string token);
    }
}