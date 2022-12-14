using System;
using Godot;
using Global.Data;
using Global.Data.EffectSystem;
using HippieFall.Game;
using HippieFall.Items;
using HippieFall.Tunnels;
using HippieFall.Characters;
using HippieFall.Effects;

namespace HippieFall
{
	public class Player : KinematicBody
	{
		[Export] private NodePath _playerControlsPath;
		[Export] private NodePath _characterPath;

		public event Action OnPlayerEndedGame; 
		public Character Character { get; private set; }
		public PlayerControls PlayerControls { get; private set; }
		
		public PlayerRewardController PlayerRewardController => PlayerControls.PlayerCollectableController.PlayerRewardController;
		public PlayerCollectableController PlayerCollectableController => PlayerControls.PlayerCollectableController;
		public PlayerItemsController PlayerItemsController => PlayerControls.PlayerItemsController;
		public PlayerEffectController PlayerEffectController => PlayerControls.PlayerEffectController;
		public PlayerMovementController PlayerMovementController => PlayerControls.PlayerMovementController;

		public override void _Ready()
		{
			PlayerControls = GetNode<PlayerControls>(_playerControlsPath);
			Character = GetNode<Character>(_characterPath);
			HippieFallUtilities.ConnectFeedbackAfterGameReadiness(this);
		}
		private void Init()
		{
			HippieFallUtilities.Game.Level.ConfigChanged += config => 
				PlayerEffectController.EffectController.AddEffect(new LevelDataChanged(config));
		}
		
		private void OnAreaEntered(Area area)
		{
			if (area.GetOwnerOrNull<Obstacle>() is Obstacle obstacle)
			{
				if (obstacle.ObstacleType == Obstacle.ObstacleTypes.Controller) 
					return;
				OnPlayerEndedGame?.Invoke();
			}
		}
	}
}
