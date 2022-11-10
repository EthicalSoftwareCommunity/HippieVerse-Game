using HippieFall.Collectables;
using Newtonsoft.Json;

namespace Global.Data.Reward
{
    public class RewardChest : Reward
    {
        [JsonProperty] public string ChestType { get; }
        [JsonProperty] public string Rarity { get; }

        
        [JsonConstructor]
        public RewardChest(string chestType = null, string rarity = null)
        {
            ChestType = chestType;
            Rarity = rarity;
        }

        public RewardChest(CollectableChestConfig config)
        {
            ChestType = config.ChestType;
            Rarity = config.ChestRarity;
        }
    }
}