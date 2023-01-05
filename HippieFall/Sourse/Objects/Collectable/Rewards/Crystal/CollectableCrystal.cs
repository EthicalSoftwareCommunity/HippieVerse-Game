using Global;
using Global.Data;
using Global.Data.EffectSystem;
using Godot;

namespace HippieFall.Collectables
{
    public class CollectableCrystal : Collectable, IEffectable
    {
        public override void ChangeConfigData(Config config)
        {
            Config = (CollectableCrystalConfig)config; 
        }
    }
}