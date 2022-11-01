using System;
using Global.Data;
using Godot;
using Global.Data.EffectSystem;
using Global.GameSystem;
using GameController = HippieFall.Game.GameController;

namespace HippieFall
{
	public class PlayerEffectController : Node, IEffectableController
	{
		public event Action<Config> EffectsUpdated; 
		public EffectsController BonusesEffectController;
		
		private Config _config;
		
		public Config Config
		{
			get => new PlayerConfig(_config);
			set => EffectsUpdated?.Invoke(value);
		}
		
		public override void _Ready()
		{
			_config = new PlayerConfig();
			BonusesEffectController = new EffectsController(Effect.EffectsTarget.Player);
			GetNode("/root").GetChild(0).Connect(nameof(GameController.GameIsReady), this, nameof(Init));
		}
		public void Init(GameController game)
		{
			game.GameEffectController.OnReceivedPlayerEffect += BonusesEffectController.AddEffect;
			BonusesEffectController.DynamicEffectAdded += ApplyDynamicEffects;
			BonusesEffectController.ConstantEffectAdded += ApplyConstantEffect;
		}
		public void ApplyDynamicEffects()
		{
			Config = BonusesEffectController.ApplyEffectsOnConfig(Config);
		}

		public void ApplyConstantEffect(Effect effect)
		{
			_config = effect.Apply(_config);
			ApplyDynamicEffects();
		}
	}
}
