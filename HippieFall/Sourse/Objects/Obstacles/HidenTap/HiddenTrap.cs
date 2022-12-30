using Godot;
using System;
using Global;
using HippieFall;

namespace HippieFall.Tunnels
{
    public class HiddenTrap : Obstacle
    {
        [Export] private NodePath _animationPlayerPath;
        [Export] private NodePath _rotatePlatformPath;
        private Spatial _rotatePlatform;
        private AnimationPlayer _animationPlayer;
    
        public override void _Ready()
        {
            _rotatePlatform = GetNode<Spatial>(_rotatePlatformPath);
            _animationPlayer = GetNode<AnimationPlayer>(_animationPlayerPath);
            _rotatePlatform.RotateY(Utilities.GetRandomNumberFloat(-10,10));
        }

        private void OnAreaEntered(Area area)
        {
            if (area.GetOwnerOrNull<Player>() is Player player)
            {
                UnHideTrap();
            }
        }

        private void UnHideTrap()
        {
            _animationPlayer.Play(_animationPlayer.GetAnimationList()[1]);
        }
    }
}

