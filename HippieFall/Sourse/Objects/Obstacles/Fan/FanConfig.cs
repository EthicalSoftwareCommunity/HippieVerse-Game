using Global.Data;
using Godot;

namespace HippieFall.Tunnels
{
    public class FanConfig:Config
    {
        [Export] public float RotationSpeed { get; set; } = 1.5f;

        public FanConfig(FanConfig config)
        {
            RotationSpeed = config.RotationSpeed;
        }

        public FanConfig()
        {
            
        }
    }
}