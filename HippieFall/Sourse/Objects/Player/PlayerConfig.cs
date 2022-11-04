using System;
using Global.Data;
using Godot;
using HippieFall.Collectables;
using HippieFall.Items;

namespace HippieFall
{
	public class PlayerConfig :Config
	{
		public ItemConfig ItemConfig { get;}
		public MovementConfig MovementConfig { get;}

		public PlayerConfig()
		{
			ItemConfig = new ItemConfig();
			MovementConfig = new MovementConfig();
		}
		public PlayerConfig(Config config)
		{
			if (config is PlayerConfig playerConfig)
			{
				ItemConfig = new ItemConfig(playerConfig.ItemConfig);
				MovementConfig = new MovementConfig(playerConfig.MovementConfig);
			}
		}
	}
}
