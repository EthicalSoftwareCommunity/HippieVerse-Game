using Global.Data;
using Global.Data.EffectSystem;
using Godot;

namespace HippieFall.Collectables
{
    class CollectableChest : Collectable, IEffectable
    {
        public override void ChangeConfigData(Config config)
        {
            Config = (CollectableChestConfig)config;
        }
    }
}