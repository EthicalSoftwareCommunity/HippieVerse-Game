using Global.Data;
using Global.Data.EffectSystem;
using Godot;

namespace HippieFall.Collectables
{
	internal class CollectableGemcoin : Collectable, IEffectable
	{
		public CollectableGemcoin()
		{
			Config = new CollectableGemcoinConfig();
		}
		
		public override void ChangeConfigData(Config config)
		{
			Config = (CollectableGemcoinConfig)config; 
		}
	}
}
