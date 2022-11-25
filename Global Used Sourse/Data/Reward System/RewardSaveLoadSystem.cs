using System;
using Global.Constants;
using Godot;
using Newtonsoft.Json;

namespace Global.Data.Reward
{
    public class RewardSaveLoadSystem
    {
        public event Action OnRewardsSaved;
        public event Action OnRewardsLoaded;
        private readonly File _file;

        public RewardSaveLoadSystem()
        {
            _file = new File();
            Directory directory = new Directory();
            if (directory.DirExists(C_RewardFolderFile.FOLDER_REWARDS_SAVES) == false)
                directory.MakeDir(C_RewardFolderFile.FOLDER_REWARDS_SAVES);
            if (_file.FileExists(C_RewardFolderFile.FILE_REWARDS) == false)
            {
                _file.Open(C_RewardFolderFile.FILE_REWARDS, File.ModeFlags.Write);
                _file.Close();
            }
        }

        public RewardData LoadRewards()
        {
            _file.Open(C_RewardFolderFile.FILE_REWARDS, File.ModeFlags.Read);
            RewardData data = JsonConvert.DeserializeObject<RewardData>(_file.GetAsText());
            _file.Close();
            return data ?? new RewardData();
        }

        public void SaveRewards(RewardData data, bool needOverwrite = false)
        {
            RewardData previousData = LoadRewards();
            if(needOverwrite == false)
                data += previousData;
            _file.Open(C_RewardFolderFile.FILE_REWARDS, File.ModeFlags.Write);
            _file.StoreString(JsonConvert.SerializeObject(data));
            _file.Close();
        }
        
    }
}