using Godot;

namespace HippieFall.Tunnels
{
    class Gate : Obstacle
    {
        [Export] private NodePath _animationPlayerPath;
        
        public float SpeedCoefficient => _speedCoefficient;
        
        private AnimationPlayer _animation;
        private float _speedCoefficient = 1;

        public override void _Ready()
        {
            _animation = GetNode<AnimationPlayer>(_animationPlayerPath);
            ObstacleType = ObstacleTypes.Controller;
        }

        private void OnOpenGateTriggerAreaEntered(Area area)
        {
            if (area.GetOwnerOrNull<Player>() is Player)
            {
                GD.Print("Animation");
                _animation.GetAnimation("Open Gate").Length = 1*_speedCoefficient;
                _animation.Play("Open Gate");
            }
        }
    }
}