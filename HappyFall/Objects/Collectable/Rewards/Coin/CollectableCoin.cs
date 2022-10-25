using Global.Data;
using Global.Data.EffectSystem;

namespace HippieFall.Collectables
{
    internal class CollectableCoin : Collectable, IEffectable
    {
        private int _value = 1;

        public CollectableCoin()
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
            get => new CollectableCoinConfig(_config);
            set => _config = value;
        }
        
        public override void ChangeConfigData(Config config)
        {
            CollectableCoinConfig coinConfig = (CollectableCoinConfig)config;
            _value = coinConfig.Value;
            Config = new CollectableConfig()
            {
                SpawnWeight = coinConfig.SpawnWeight,
                SpawnOffsetX = coinConfig.SpawnOffsetX,
                SpawnOffsetY = coinConfig.SpawnOffsetY,
                SpawnOffsetZ = coinConfig.SpawnOffsetZ,
            };
        }
    }
}