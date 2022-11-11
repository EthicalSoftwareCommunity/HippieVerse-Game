using System.Collections.Generic;
using Global.Data;
using Godot;
using Global.Data.EffectSystem;
using HippieFall.Collectables;
using HippieFall.Items;

namespace HippieFall.Tunnels
{
	public class Magnet : Effect
	{
		private float _force;
		public Magnet(float force = 10f)
		{
			Target = EffectsTarget.Player;
			_force = force;
		}

		public override Config Apply(Config config)
		{
			if (config is PlayerConfig playerConfig)
			{
				playerConfig.ItemConfig.MagnetConfig.IsMagnetActivated = true;
				playerConfig.ItemConfig.MagnetConfig.Force = _force;
				return playerConfig;
			}
			return config;
		}
	}
}
