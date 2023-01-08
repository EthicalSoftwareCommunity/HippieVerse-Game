using Global;
using Global.Data;
using Global.Data.EffectSystem;
using Godot;

namespace HippieFall.Tunnels
{
    
    class Saw : Obstacle, IEffectable
    {
        [Export] private NodePath _movingObstaclePath;
        [Export] private NodePath _rotatingObstaclePath;
        private Spatial _movingObstacle;
        private Spatial _rotatingObstacle;
        private float _d;
        private float _x;
        private float _y;
        private int _direction;

        public override void _Ready()
        {
            _movingObstacle = GetNode<Spatial>(_movingObstaclePath);
            _rotatingObstacle = GetNode<Spatial>(_rotatingObstaclePath);
            _direction = Utilities.GetRandomNumberInt(-1, 1, Utilities.Parameter.WITH_OUT_ZERO);
            _d = Utilities.GetRandomNumberFloat(-180, 180);
        }
        
        public override void _PhysicsProcess(float delta)
        {
            CircleMoving(delta);
        }

        private void CircleMoving(float delta)
        {
            //x^2 + z^2 = radius^2
            _d += delta * ((SawConfig)Config).Speed * Mathf.Sign(_direction);
            _x = Mathf.Sin(_d) * ((SawConfig)Config).Radius;
            _y = Mathf.Cos(_d) * ((SawConfig)Config).Radius;
            _rotatingObstacle.RotateZ(((SawConfig)Config).Speed);
            _movingObstacle.Translation = new Vector3(_x, _y, _movingObstacle.Translation.z);
        }

        public void ChangeConfigData(Config config)
        {
            if (config is SawConfig obstacleConfig)
            {
                Config = new SawConfig(obstacleConfig);
            }
        }
    }
}