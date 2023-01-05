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

		[Export] private CollectableCoinConfig _coinConfig;
		[Export] private CollectableCrystalConfig _crystalConfig;
		[Export] private CollectableCrystalConfig _crystalDepositConfig;
		[Export] private CollectableChestConfig _chestConfig;

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
				_crystalConfig,
				_chestConfig,
				_crystalDepositConfig
			});
			HippieFallUtilities.Game.GameEffectController.OnReceivedCollectableEffect += EffectController.AddEffect;
		}
		
		public override Config GetConfigByType(Node node)
		{
			if (node is CollectableCoin)
				return new CollectableCoinConfig(_coinConfig);
			if (node is CollectableCrystalDeposit)
				return new CollectableCrystalDepositConfig(_crystalDepositConfig);
			if (node is CollectableCrystal collectableCrystal)
			{
				if (collectableCrystal.Config is CollectableCrystalDepositConfig)
					return new CollectableCrystalDepositConfig(_crystalDepositConfig);
				if (collectableCrystal.Config is CollectableCrystalConfig)
					return new CollectableCrystalConfig(_crystalConfig);
			}
			if (node is CollectableChest)
				return new CollectableChestConfig(_chestConfig);
			return null;
		}

	}
}
