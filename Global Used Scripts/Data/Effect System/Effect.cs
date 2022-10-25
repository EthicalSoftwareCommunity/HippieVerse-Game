using Godot;

namespace Global.Data.EffectSystem
{
    public abstract class Effect : Spatial
    {
        public enum EffectsTarget
        {
            Level,
            Obstacles,
            Player,
            Collectable,
            Character
        }
        
        public EffectsTarget Target;
        
        public abstract Config Apply(Config config);
    }
}