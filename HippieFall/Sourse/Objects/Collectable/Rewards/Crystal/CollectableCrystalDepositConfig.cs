using Global;
using Godot;

namespace HippieFall.Collectables
{
    public class CollectableCrystalDepositConfig : CollectableCrystalConfig
    {
        [Export] private int MaxValue { get; set; } = 10;
        [Export] private int MinValue { get; set; } = 4;
        
        private int _value = 0;
        public override int Value
        {
            get => _value * Utilities.GetRandomNumberInt(MinValue, MaxValue);
            set => _value = value;
        }

        public CollectableCrystalDepositConfig()
        {
            
        }
        public CollectableCrystalDepositConfig(CollectableConfig collectableConfig) : base(collectableConfig)
        {
            if (collectableConfig is CollectableCrystalConfig config)
                Value = config.Value;
        }
    }
}