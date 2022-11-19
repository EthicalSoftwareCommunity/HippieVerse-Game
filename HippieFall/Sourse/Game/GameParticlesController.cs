using Godot;
using System;
using System.Collections.Generic;
using Global.Data;
using Global.Data.EffectSystem;
using HippieFall.Effects;

namespace HippieFall.Game
{
    public class GameParticlesController : Node
    {
        [Export] private NodePath _windFallPath;
        [Export] private NodePath _windFallFastPath;
        private Particles _windFall;
        private Particles _windFallFast;
        private readonly LevelConfig _startLevelConfig = new LevelConfig();
        public override void _Ready()
        {
            _windFall = GetNode<Particles>(_windFallPath);
            _windFallFast = GetNode<Particles>(_windFallFastPath);
            _windFallFast.Emitting = false;
            GetNode("/root").GetChild(0).Connect(nameof(GameController.GameIsReady), this, nameof(Init));
        }

        private void Init(GameController game)
        {
            game.Level.ConfigChanged += UpdateCharacteristicsByLevelSpeed;
            game.GameEffectController.OnReceivedNamedEffect += ReactToEffect;
        }

        private void ReactToEffect(NamedEffect namedEffect)
        {
            if (namedEffect is LongTapedOnDisplay longTapedOnDisplay)
            {
                _windFall.Emitting = false;
                _windFallFast.Emitting = true;
                longTapedOnDisplay.OnNamedEffectRemoved += DisableReactToEffect;
            }
        }

        private void DisableReactToEffect(NamedEffect namedEffect)
        {
            if (namedEffect is LongTapedOnDisplay speedTwice)
            {
                _windFall.Emitting = true;
                _windFallFast.Emitting = false;
            }
        }

        private void UpdateCharacteristicsByLevelSpeed(Config config)
        {
            if (config is LevelConfig levelConfig)
            {
                float differenceCoefficient = levelConfig.Speed / _startLevelConfig.Speed;
                _windFall.SpeedScale *= differenceCoefficient<3 ? 1  : differenceCoefficient/3 ;
            }
        }
    }
}
