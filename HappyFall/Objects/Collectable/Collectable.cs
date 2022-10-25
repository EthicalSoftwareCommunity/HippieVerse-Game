using Global.Data;
using Global.Data.EffectSystem;
using Godot;

namespace HippieFall.Collectables
{
    public abstract class Collectable : Spatial, IEffectable
    {
        protected CollectableConfig _config;
        public virtual CollectableConfig Config { get; set; }
        
        protected virtual void OnAreaEntered(Area area)
        {
            if (area.GetParentOrNull<Player>() != null) QueueFree();
        }

        public abstract void ChangeConfigData(Config config);
    }
}