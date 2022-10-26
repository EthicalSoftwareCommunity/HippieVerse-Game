using Global.Constants;
using Godot;
using Newtonsoft.Json;

namespace Global.Data.Reward
{
    public class RewardSaveLoadSystem
    {
        private readonly File _file;

        public RewardSaveLoadSystem()
        {
            _file = new File();
            new Directory().MakeDir(C_RewardFolderFile.FOLDER_REWARDS_SAVES);
        }

        public RewardData LoadRewards()
        {
            _file.Open(C_RewardFolderFile.FILE_REWARDS, File.ModeFlags.Read);
            RewardData data = JsonConvert.DeserializeObject<RewardData>(_file.GetAsText());
            _file.Close();
            return data ?? new RewardData();
        }

        public void SaveRewards(RewardData data)
        {
            RewardData previousData = LoadRewards();
            data += previousData;
            _file.Open(C_RewardFolderFile.FILE_REWARDS, File.ModeFlags.Write);
            _file.StoreString(JsonConvert.SerializeObject(data));
            _file.Close();
        }
    }
}