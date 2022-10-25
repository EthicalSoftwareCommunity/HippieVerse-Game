using Newtonsoft.Json;

namespace Global.Data.Reward
{
    public class RewardChest : Reward
    {
        public string ChestType { get; }
        public string Rarity { get; }

        public RewardChest(string chestType = null, string rarity = null)
        {
            ChestType = chestType;
            Rarity = rarity;
        }
    }
}