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
		public ObstaclesController():base(Effect.EffectsTarget.Obstacles)
		{
			
		}

		public override void Init(Node node = null, Config config = null)
		{
			HippieFallUtilities.Game.GameEffectController.OnReceivedObstaclesEffect += EffectController.AddEffect;
		}
	}
}
