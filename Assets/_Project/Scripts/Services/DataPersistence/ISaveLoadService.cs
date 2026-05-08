using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Services.DataPersistence
{
    public interface ISaveLoadService
    {
        UniTask<SaveData> Load();
        UniTask Save(SaveData data);
    }
}