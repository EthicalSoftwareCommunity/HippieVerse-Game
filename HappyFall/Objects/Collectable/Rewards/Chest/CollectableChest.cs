using Global.Data;
using Global.Data.EffectSystem;
using Godot;

namespace HippieFall.Collectables
{
    class CollectableChest : Collectable,IEffectable
    {
        private int _value = 1;
        public string ChestRarity { get; set; } = "Rare";
        public string ChestType { get; set; } = "Common";

        public CollectableChest()
        {
            Config = new CollectableChestConfig();
        }

        public override CollectableConfig Config
        {
            get => new CollectableChestConfig();
            set => _config = value;
        }

        public override void ChangeConfigData(Config config)
        {
            CollectableChestConfig chestConfig = (CollectableChestConfig)config;
            _value = chestConfig.Value;
        }
    }
}