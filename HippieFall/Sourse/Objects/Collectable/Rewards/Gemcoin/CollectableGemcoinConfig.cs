using Global.Data;

namespace HippieFall.Collectables
{
	public class CollectableGemcoinConfig : CollectableConfig
	{
		public int Value { get; set; }= 1;
		public CollectableGemcoinConfig()
		{
			SpawnWeight = 100f;
			SpawnOffsetX = 1f;
			SpawnOffsetZ = 1;
		}
		public CollectableGemcoinConfig(CollectableConfig collectableConfig) : base(collectableConfig)
		{
			if(collectableConfig is CollectableGemcoinConfig config)
				Value = config.Value;
		}
	}
}
