using Global.Data;
using Godot;

namespace HippieFall.Tunnels
{
    public class SawConfig:Config
    {
        [Export] public float Speed { get; set; }= 1.5f;
        [Export] public float Radius { get; set; } = 3.2f;
        public SawConfig(SawConfig config)
        {
            Speed = config.Speed;
            Radius = config.Radius;
        }

        public SawConfig()
        {
            
        }
    }
}