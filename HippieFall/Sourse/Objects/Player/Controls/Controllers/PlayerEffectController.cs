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
		private PlayerConfig _playerConfig;
		public PlayerEffectController() : base(Effect.EffectsTarget.Player)
		{
			
		}

		public  override void Init(Node node, Config config)
		{
			_playerConfig = (PlayerConfig)config;
			AddNode(node, _playerConfig);
			HippieFallUtilities.Game.GameEffectController.OnReceivedPlayerEffect += EffectController.AddEffect;
		}
	}
}
