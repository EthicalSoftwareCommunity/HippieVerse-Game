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
		
		public override void _Ready()
		{
			_playerConfig = new();
			Configs.Add(_playerConfig);
			HippieFallUtilities.ConnectFeedbackAfterGameReadiness(this, nameof(Init));
		}
		protected override void Init()
		{
			AddNode(HippieFallUtilities.Game.Player.PlayerControls);
			HippieFallUtilities.Game.GameEffectController.OnReceivedPlayerEffect += EffectController.AddEffect;
		}

		protected override Config GetConfigByType(Node node)
		{
			if (node is PlayerControls) 
				return new PlayerConfig(_playerConfig);
			return null;
		}

	}
}
