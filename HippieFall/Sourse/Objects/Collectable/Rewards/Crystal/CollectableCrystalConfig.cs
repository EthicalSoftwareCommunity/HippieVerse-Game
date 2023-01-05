using Global.Data;
using Godot;

namespace HippieFall.Collectables
{
    public class CollectableCrystalConfig : CollectableConfig
    {
        public virtual int Value { get => _value; set => _value = value; }
        [Export] protected int _value = 1;

        public CollectableCrystalConfig()
        {
            
        }
        
        public CollectableCrystalConfig(CollectableConfig collectableConfig) : base(collectableConfig)
        {
            if(collectableConfig is CollectableCrystalConfig config)
                Value = config.Value;
        }
    }
}