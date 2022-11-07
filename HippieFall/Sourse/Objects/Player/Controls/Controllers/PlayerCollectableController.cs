using Godot;
using System;
using System.Collections.Generic;
using Global.Data.EffectSystem;
using HippieFall.Collectables;
using HippieFall.Tunnels;

namespace HippieFall
{
	public class PlayerCollectableController : Node
	{
		[Export] private NodePath PlayerRewardControllerPath;
		
		public event Action<List<Effect>> OnBonusCollected;
		
		public PlayerRewardController PlayerRewardController { get; private set; }
		
		public override void _Ready()
		{
			PlayerRewardController = GetNode<PlayerRewardController>(PlayerRewardControllerPath);
		}

		private void OnAreaEntered(Area area)
		{
			if (area.GetOwnerOrNull<Bonus>() is Bonus bonus)
				OnBonusCollected?.Invoke(bonus.Effects);
		}
	}

}
