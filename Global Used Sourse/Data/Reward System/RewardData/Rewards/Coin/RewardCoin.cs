using Newtonsoft.Json;

namespace Global.Data.Reward
{
    public class RewardCoin : Reward
    {
        public int Count { get; set; }
        public RewardCoin(int count = 0)
        {
            Count = count;
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