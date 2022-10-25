using Global.Data;
using Global.Data.EffectSystem;

namespace HippieFall.Tunnels
{
    class Fan : Obstacle, IEffectable
    {
        private float _rotationSpeed;

        public override void _PhysicsProcess(float delta)
        {
            RotateY(_rotationSpeed * delta);
        }

        public void ChangeConfigData(Config config)
        {
            if (config is FanConfig obstacleConfig)
            {
                _rotationSpeed = obstacleConfig.RotationSpeed;
            }
        }
    }
}