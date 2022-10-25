using Newtonsoft.Json;

namespace Global.Data.Reward
{
    public class RewardGemcoin : Reward
    {
        public int Count { get; set; }
        public RewardGemcoin(int count = 0)
        {
            Count = count;
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