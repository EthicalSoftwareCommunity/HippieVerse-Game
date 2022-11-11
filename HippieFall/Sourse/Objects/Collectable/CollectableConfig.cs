using System;
using Global.Data;
using Global.Data.EffectSystem;

namespace HippieFall.Collectables
{
	public class CollectableConfig : Config, IEffectable
	{
		public float SpawnOffsetX { get; set; } = 0;
		public float SpawnOffsetZ { get; set; } = 0;
		public float SpawnOffsetY { get; set; } = 0;
		public float SpawnWeight { get; set; } = 10;

		public CollectableConfig()
		{
		}

		public CollectableConfig(Config config)
		{
			if (config is CollectableConfig collectableConfig)
			{
				SpawnWeight = collectableConfig.SpawnWeight;
				SpawnOffsetX = collectableConfig.SpawnOffsetX;
				SpawnOffsetY = collectableConfig.SpawnOffsetY;
				SpawnOffsetZ = collectableConfig.SpawnOffsetZ;
			}
		}

		public void ChangeConfigData(Config config)
		{
			if (config == null) throw new ArgumentNullException(nameof(config));
			if (config is CollectableConfig collectableConfig)
			{
				if (collectableConfig.SpawnWeight == -1f)
					SpawnWeight = collectableConfig.SpawnWeight;
				if (collectableConfig.SpawnOffsetX == -1f)
					SpawnOffsetX = collectableConfig.SpawnOffsetX;
				if (collectableConfig.SpawnOffsetY == -1f)
					SpawnOffsetY = collectableConfig.SpawnOffsetY;
				if (collectableConfig.SpawnOffsetZ == -1f)
					SpawnOffsetZ = collectableConfig.SpawnOffsetZ;
			}
		}
	}
}
