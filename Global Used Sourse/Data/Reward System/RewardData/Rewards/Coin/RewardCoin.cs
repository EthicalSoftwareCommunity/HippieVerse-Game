using HippieFall.Collectables;
using Newtonsoft.Json;

namespace Global.Data.Reward
{
    public class RewardCoin : Reward
    {
        [JsonProperty] public int Count { get; set; }
        [JsonConstructor]
        public RewardCoin(int count = 0)
        {
            Count = count;
        }
        public RewardCoin(CollectableCoinConfig config)
        {
            Count = config.Value;
        }
        
        public static RewardCoin operator +(RewardCoin left, RewardCoin right)
        {
            return new RewardCoin
            {
                Count = left.Count + right.Count
            };
        }
    }
}