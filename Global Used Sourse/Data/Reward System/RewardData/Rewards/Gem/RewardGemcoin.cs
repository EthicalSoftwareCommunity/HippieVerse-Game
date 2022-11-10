using HippieFall.Collectables;
using Newtonsoft.Json;

namespace Global.Data.Reward
{
    public class RewardGemcoin : Reward
    {
        [JsonProperty] public int Count { get; set; }
        [JsonConstructor]
        public RewardGemcoin(int count = 0)
        {
            Count = count;
        }

        public RewardGemcoin(CollectableGemcoinConfig config)
        {
            Count = config.Value;
        }
        public static RewardGemcoin operator +(RewardGemcoin left, RewardGemcoin right)
        {
            return new RewardGemcoin
            {
                Count = left.Count + right.Count
            };
        }
    }
}