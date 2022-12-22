using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;

namespace Global.Data.Reward
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RewardData : Node
    {
        public RewardData()
        {
            Coin = new RewardCoin();
            Gemcoin = new RewardGemcoin();
            Chests = new List<RewardChest>();
        }

        public RewardData(RewardData data)
        {
            Coin = data.Coin;
            Gemcoin = data.Gemcoin;
            Chests = data.Chests;
        }

        [JsonProperty] public List<RewardChest> Chests { get; private set; }

        [JsonProperty] public RewardCoin Coin { get; private set; }

        [JsonProperty] public RewardGemcoin Gemcoin { get; private set; }

        public void AddChest(RewardChest chest)
        {
            Chests.Add(chest);
        }

        public void AddCoin(RewardCoin coin)
        {
            Coin.Count += coin.Count;
        }

        public void AddGemcoin(RewardGemcoin gemcoin)
        {
            Gemcoin.Count += gemcoin.Count;
        }

        public static RewardData operator +(RewardData left, RewardData right)
        {
            left.Chests.AddRange(right.Chests);
            return new RewardData
            {
                Coin = left.Coin + right.Coin,
                Gemcoin = left.Gemcoin + right.Gemcoin,
                Chests = left.Chests
            };
        }
    }
}