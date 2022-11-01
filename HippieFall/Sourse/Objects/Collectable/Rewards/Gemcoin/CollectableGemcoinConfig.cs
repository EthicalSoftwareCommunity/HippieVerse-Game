using Global.Data;

namespace HippieFall.Collectables
{
    public class CollectableGemcoinConfig : CollectableConfig
    {
        public int Value = 1;
        public CollectableGemcoinConfig()
        {
            SpawnWeight = 100f;
            SpawnOffsetX = 1f;
            SpawnOffsetZ = 1;
        }
    }
}