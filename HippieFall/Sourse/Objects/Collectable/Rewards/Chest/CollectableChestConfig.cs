using Global.Data;

namespace HippieFall.Collectables
{
    public class CollectableChestConfig : CollectableConfig
    {
        public int Value { get; set; }= 1;
        public string ChestRarity { get; set; } = "Rare";
        public string ChestType { get; set; } = "Common";
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