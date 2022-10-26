using Global.Data;

namespace HippieFall.Tunnels
{
    public class SawConfig:Config
    {
        public float Speed { get; set; }= 1.5f;
        public float Radius { get; set; } = 3.2f;
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