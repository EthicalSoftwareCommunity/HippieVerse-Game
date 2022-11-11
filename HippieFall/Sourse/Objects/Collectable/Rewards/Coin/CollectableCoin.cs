using Global.Data;
using Global.Data.EffectSystem;

namespace HippieFall.Collectables
{
    internal class CollectableCoin : Collectable, IEffectable
    {
        public CollectableCoin()
        {
            Config = new CollectableCoinConfig();
        }
        
        public override void ChangeConfigData(Config config)
        {
            Config = (CollectableCoinConfig)config; 
        }
    }
}