using Global.Data;
using Godot;

namespace HippieFall.Collectables
{
    public class CollectableCrystalConfig : CollectableConfig
    {
        [Export] public virtual int Value { get; set; } = 1;

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