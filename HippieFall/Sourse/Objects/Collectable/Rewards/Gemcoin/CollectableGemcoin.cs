using Global.Data;
using Global.Data.EffectSystem;
using Godot;

namespace HippieFall.Collectables
{
    internal class CollectableGemcoin : Collectable, IEffectable
    {
        [Export] private int _value = 1;

        public CollectableGemcoin()
        {
            Config = new CollectableConfig
            {
                SpawnWeight = 200f,
                SpawnOffsetX = 1.5f,
                SpawnOffsetZ = 1.5f
            };
        }
        public override CollectableConfig Config
        {
            get => new CollectableGemcoinConfig();
            set => _config = value;
        }
        public override void ChangeConfigData(Config config)
        {
            CollectableGemcoinConfig gemcoinConfig = (CollectableGemcoinConfig)config;
            _value = gemcoinConfig.Value;
        }
    }
}