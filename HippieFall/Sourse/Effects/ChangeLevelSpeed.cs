using System.Runtime.InteropServices;
using Global.Data;
using Global.Data.EffectSystem;
using HippieFall.Game;

namespace HippieFall.Effects
{
    public class ChangeLevelSpeed:Effect
    {
        private readonly float _delta;
        private readonly bool _isCoefficient = false;
        public ChangeLevelSpeed(float delta = 0.1f, bool isCoefficient = false)
        {
            Target = EffectsTarget.Level;
            _delta = delta;
            _isCoefficient = isCoefficient;
        }

        public override Config Apply(Config config)
        {
            if (config is LevelConfig levelConfig)
            {
                if(_isCoefficient)
                    levelConfig.Speed *= _delta;
                else 
                    levelConfig.Speed += _delta;
                return levelConfig;
            }
            return config;
        }
    }
}