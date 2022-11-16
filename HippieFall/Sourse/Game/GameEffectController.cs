using System;
using System.Collections.Generic;
using Global.Data.EffectSystem;
using Godot;
using HippieFall.Game;

namespace HippieFall
{
	public class GameEffectController : Node
	{
		public event Action<Effect> OnReceivedPlayerEffect;
		public event Action<Effect> OnReceivedLevelEffect;
		public event Action<Effect> OnReceivedCharacterEffect;
		public event Action<Effect> OnReceivedCollectableEffect;
		public event Action<Effect> OnReceivedObstaclesEffect;
		public event Action<NamedEffect> OnReceivedNamedEffect;

		public override void _Ready()
		{
			GetNode("/root").GetChild(0).Connect(nameof(GameController.GameIsReady), this, nameof(SetDependencies));
		}

		public void SetDependencies(GameController game)
		{
			game.Player.PlayerCollectableController.OnBonusCollected += ResendEvent;
			game.Level.OnLevelEffectAdded += ResendEvent;
			game.Player.Character.OnCharacterEffectAdded += ResendEvent;
			game.Player.PlayerMovementController.OnMovementEffectAdded += ResendEvent;
		}

		private void ResendEvent(NamedEffect namedEffect)
		{
			ResendEvent(namedEffect.Effects);
			OnReceivedNamedEffect?.Invoke(namedEffect);
		}
		private void ResendEvent(List<Effect> effects)
		{
			foreach (var effect in effects)
			{
				switch (effect.Target)
				{
					case Effect.EffectsTarget.Level:  OnReceivedLevelEffect?.Invoke(effect); break;
					case Effect.EffectsTarget.Player:  OnReceivedPlayerEffect?.Invoke(effect); break;
					case Effect.EffectsTarget.Collectable:  OnReceivedCollectableEffect?.Invoke(effect); break;
					case Effect.EffectsTarget.Obstacles:  OnReceivedObstaclesEffect?.Invoke(effect); break;
					case Effect.EffectsTarget.Character:  OnReceivedCharacterEffect?.Invoke(effect); break;
				}
			}
		}
	}
}
