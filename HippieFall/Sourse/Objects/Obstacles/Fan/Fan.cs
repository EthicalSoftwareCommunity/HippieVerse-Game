using Global.Data;
using Global.Data.EffectSystem;
using HippieFall.Collectables;

namespace HippieFall.Tunnels
{
    class Fan : Obstacle, IEffectable
    {
        public override void _PhysicsProcess(float delta)
        {
            RotateY(((FanConfig)Config).RotationSpeed * delta);
        }

        public void ChangeConfigData(Config config)
        {
            if (config is FanConfig obstacleConfig)
            {
                Config = new FanConfig(obstacleConfig);
            }
        }
    }
}