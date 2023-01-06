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

		public CollectableController() : base(Effect.EffectsTarget.Collectable)
		{
		}
		
		public override void Init(Node node = null, Config config = null)
		{
			CollectableSpawner = new CollectableSpawner();
			CollectableSpawner.Init();
			CollectableSpawner.OnCollectableCreated += AddNode;
			HippieFallUtilities.Game.GameEffectController.OnReceivedCollectableEffect += EffectController.AddEffect;
		}
	}
}
