using Global.Data;
using Godot;

namespace HippieFall.Collectables
{
    public class CollectableChestConfig : CollectableConfig
    {
        [Export] public int Value { get; set; }= 1;
        [Export] public string ChestRarity { get; set; } = "Rare";
        [Export] public string ChestType { get; set; } = "Common";
        public CollectableChestConfig() 
        {
            SpawnWeight = 100f;
            SpawnOffsetX = 1f;
            SpawnOffsetZ = 1;
        }
        public CollectableChestConfig(CollectableConfig collectableConfig) : base(collectableConfig)
        {
            if (collectableConfig is CollectableChestConfig config)
            {
                Value = config.Value;
                ChestRarity = config.ChestRarity;
                ChestType = config.ChestType;
            }
        }
    }
}