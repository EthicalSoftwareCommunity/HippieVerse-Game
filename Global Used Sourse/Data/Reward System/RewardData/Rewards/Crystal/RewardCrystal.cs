using HippieFall.Collectables;
using Newtonsoft.Json;

namespace Global.Data.Reward
{
    public class RewardCrystal : Reward
    {
        [JsonProperty] public int Count { get; set; }
        [JsonConstructor]
        public RewardCrystal(int count = 0)
        {
            Count = count;
        }

        public RewardCrystal(CollectableCrystalConfig config)
        {
            Count = config.Value;
        }
        public static RewardCrystal operator +(RewardCrystal left, RewardCrystal right)
        {
            return new RewardCrystal
            {
                Count = left.Count + right.Count
            };
        }
    }
}