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
            Crystal = new RewardCrystal();
            Chests = new List<RewardChest>();
        }

        public RewardData(RewardData data)
        {
            Coin = data.Coin;
            Crystal = data.Crystal;
            Chests = data.Chests;
        }

        [JsonProperty] public List<RewardChest> Chests { get; private set; }

        [JsonProperty] public RewardCoin Coin { get; private set; }

        [JsonProperty] public RewardCrystal Crystal { get; private set; }

        public void AddChest(RewardChest chest)
        {
            Chests.Add(chest);
        }

        public void AddCoin(RewardCoin coin)
        {
            Coin.Count += coin.Count;
        }

        public void AddCrystal(RewardCrystal crystal)
        {
            Crystal.Count += crystal.Count;
        }

        public static RewardData operator +(RewardData left, RewardData right)
        {
            left.Chests.AddRange(right.Chests);
            return new RewardData
            {
                Coin = left.Coin + right.Coin,
                Crystal = left.Crystal + right.Crystal,
                Chests = left.Chests
            };
        }
    }
}