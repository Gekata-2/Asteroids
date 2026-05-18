namespace _Project.Scripts.Services.DataPersistence
{
    public class SavesData
    {
        public SaveData LocalSave { get; }
        public SaveData CloudSave { get; }
        public bool IsSyncingRequired { get; }

        public SavesData(SaveData localSave, SaveData cloudSave)
        {
            LocalSave = localSave;
            CloudSave = cloudSave;
            if (CloudSave == null)
                IsSyncingRequired = false;
            else
                IsSyncingRequired = LocalSave.Date != cloudSave.Date;
        }
    }
}