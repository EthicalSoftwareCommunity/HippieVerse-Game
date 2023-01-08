using System;
using Global.Data;
using Godot;
using Global.Data.EffectSystem;
using Global.GameSystem;
using HippieFall.Game;
using GameController = HippieFall.Game.GameController;

namespace HippieFall
{
	public class PlayerEffectController : ObjectEffectController
	{
		public PlayerEffectController() : base(Effect.EffectsTarget.Player)
		{
			
		}

		public  override void Init(Node node, Config config)
		{
			AddNode(node, (PlayerConfig)config);
			HippieFallUtilities.Game.GameEffectController.OnReceivedPlayerEffect += EffectController.AddEffect;
		}
	}
}
