using Global;
using Global.Data;
using Global.Data.EffectSystem;
using Godot;

namespace HippieFall.Collectables
{
    public class CollectableCoin : Collectable, IEffectable
    {
        private AnimationPlayer _animationPlayer;
        public override void _Ready()
        {
            SetAnimationSettings();
        }
        
        public override void ChangeConfigData(Config config)
        {
            Config = (CollectableCoinConfig)config; 
        }

        private void SetAnimationSettings()
        {
            _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
            _animationPlayer.PlaybackSpeed =
                Utilities.GetRandomNumberFloat(-1f, 1f);
            if (Mathf.Abs(_animationPlayer.PlaybackSpeed) < 0.5f)
                _animationPlayer.PlaybackSpeed = 1 - Mathf.Abs(_animationPlayer.PlaybackSpeed) *
                    Mathf.Sign(_animationPlayer.PlaybackSpeed);
        }
    }
}