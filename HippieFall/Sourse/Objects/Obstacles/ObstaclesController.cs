using System.Collections.Generic;
using Global.Data;
using Godot;
using Global.Data.EffectSystem;
using Global.GameSystem;
using HippieFall.Game;
using GameController = HippieFall.Game.GameController;

namespace HippieFall.Tunnels
{
	public class ObstaclesController : ObjectEffectController
	{
		private FanConfig _fan;
		private SawConfig _saw;
		private PerforatedWallConfig _perforatedWall;

		public ObstaclesController():base(Effect.EffectsTarget.Obstacles)
		{
			
		}
		public override void _Ready()
		{
			_fan = new FanConfig();
			_saw = new SawConfig();
			_perforatedWall = new PerforatedWallConfig();
			Configs.Add(_fan);
			Configs.Add(_saw);
			Configs.Add(_perforatedWall);
			HippieFallUtilities.ConnectFeedbackAfterGameReadiness(this, nameof(Init));
		}

		protected override void Init()
		{
			HippieFallUtilities.Game.GameEffectController.OnReceivedObstaclesEffect += EffectController.AddEffect;
		}

		protected override Config GetConfigByType(Node node)
		{
			if (node is Fan) return new FanConfig(_fan);
			if (node is Saw) return new SawConfig(_saw);
			if (node is PerforatedWall) return  new PerforatedWallConfig(_perforatedWall);
			return null;
		}
	}
}
