using Global.Data;

namespace HippieFall.Collectables
{
    public class CollectableCoinConfig : CollectableConfig
    {
        public int Value { get; set; } = 1;
        public CollectableCoinConfig()
        {
            SpawnWeight = 100f;
            SpawnOffsetX = 1f;
            SpawnOffsetZ = 1;
        }
        public CollectableCoinConfig(CollectableConfig collectableConfig)
        {
            SpawnWeight = collectableConfig.SpawnWeight;
            SpawnOffsetX = collectableConfig.SpawnOffsetX;
            SpawnOffsetZ = collectableConfig.SpawnOffsetZ;
        }
    }
}