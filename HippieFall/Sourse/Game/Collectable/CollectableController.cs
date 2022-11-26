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
	
		public CollectableSpawner CollectableSpawner { get; private set; }

		private CollectableCoinConfig _coinConfig = new ();
		private CollectableGemcoinConfig _gemCoinConfig = new ();
		private CollectableChestConfig _chestConfig = new ();

		public CollectableController() : base(Effect.EffectsTarget.Collectable)
		{
		}
		
		public override void Init(Node node = null, Config config = null)
		{
			CollectableSpawner = new CollectableSpawner();
			CollectableSpawner.Init();
			CollectableSpawner.OnCollectableCreated += AddNode;
			Configs.AddRange(new List<Config>()
			{
				_coinConfig,
				_gemCoinConfig,
				_chestConfig
			});
			HippieFallUtilities.Game.GameEffectController.OnReceivedCollectableEffect += EffectController.AddEffect;
		}
		
		public override Config GetConfigByType(Node node)
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
