using Global.Data;

namespace HippieFall.Tunnels
{
    public class SawConfig:Config
    {
        public float Speed = 1.5f;
        public float Radius = 2.6f;
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