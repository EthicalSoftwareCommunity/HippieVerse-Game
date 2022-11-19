using System.CodeDom;
using System.Collections.Generic;
using System.Linq.Expressions;
using Global.Data;
using Godot;
using Global.Data.EffectSystem;
using Global.GameSystem;
using HippieFall.Collectables;
using HippieFall.Game;
using HippieFall.Tunnels;
using GameController = HippieFall.Game.GameController;

namespace HippieFall
{
	public class CollectableController : ObjectEffectController
	{
	
		[Export] private NodePath _collectableSpawnerPath;
		public CollectableSpawner CollectableSpawner { get; private set; }

		private CollectableCoinConfig _coinConfig = new ();
		private CollectableGemcoinConfig _gemCoinConfig = new ();
		private CollectableChestConfig _chestConfig = new ();

		public CollectableController() : base(Effect.EffectsTarget.Collectable)
		{
		}
		public override void _Ready()
		{
			CollectableSpawner = GetNode<CollectableSpawner>(_collectableSpawnerPath);
			CollectableSpawner.OnCollectableCreated += AddNode;
			Configs.AddRange(new List<Config>()
			{
				_coinConfig,
				_gemCoinConfig,
				_chestConfig
			});
			HippieFallUtilities.ConnectFeedbackAfterGameReadiness(this, nameof(Init));
		}

		protected override void Init()
		{
			HippieFallUtilities.Game.GameEffectController.OnReceivedCollectableEffect += EffectController.AddEffect;
		}

		protected override Config GetConfigByType(Node node)
		{
			if (node is CollectableCoin)
				return new CollectableCoinConfig(_coinConfig);
			if (node is CollectableGemcoin)
				return new CollectableGemcoinConfig(_gemCoinConfig);
			if (node is CollectableChest)
				return new CollectableChestConfig(_chestConfig);
			return null;
		}

	}
}
