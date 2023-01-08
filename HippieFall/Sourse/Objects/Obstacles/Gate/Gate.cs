using Global;
using Global.Data;
using Godot;
using HippieFall.Game;

namespace HippieFall.Tunnels
{
    public class Gate : Obstacle
    {
        [Export] private NodePath _animationPlayerPath;

        private AnimationPlayer _animation;
        private LevelConfig _levelConfig;
        private float _speedDifferenceCoefficient = 0;
        private GameController _gameController;

        public override void _Ready()
        {
            _animation = GetNode<AnimationPlayer>(_animationPlayerPath);
            ObstacleType = ObstacleTypes.Controller;
            if(HippieFallUtilities.Game.IsGameIsReady) _levelConfig = HippieFallUtilities.Game.Level.LevelConfig;
            else HippieFallUtilities.ConnectFeedbackAfterGameReadiness(this);

            Config = new Config();
        }

        private void Init()
        {
            _levelConfig = HippieFallUtilities.Game.Level.LevelConfig;
        }

        private void OnOpenGateTriggerAreaEntered(Area area)
        {
            if (area.GetOwnerOrNull<Player>() is Player)
            {
                GD.Print("Animation");
                _speedDifferenceCoefficient =
                    HippieFallUtilities.Game.Level.LevelConfig.Speed / _levelConfig.Speed;
                _animation.PlaybackSpeed = _speedDifferenceCoefficient;
                _animation.Play("Open Gate");
            }
        }
    }
}