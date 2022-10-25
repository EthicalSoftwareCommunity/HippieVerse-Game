using Global.Data;

namespace HippieFall.Tunnels
{
    public class FanConfig:Config
    {
        public float RotationSpeed { get; set; }= 1.5f;

        public FanConfig(FanConfig config)
        {
            RotationSpeed = config.RotationSpeed;
        }

        public FanConfig()
        {
            
        }
    }
}