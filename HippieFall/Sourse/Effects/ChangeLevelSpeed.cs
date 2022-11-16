using System.Runtime.InteropServices;
using Global.Data;
using Global.Data.EffectSystem;
using HippieFall.Game;

namespace HippieFall.Effects
{
    public class ChangeLevelSpeed:Effect
    {
        public readonly float Delta;
        public readonly bool IsCoefficient = false;
        public ChangeLevelSpeed(float delta = 0.1f, bool isCoefficient = false)
        {
            Target = EffectsTarget.Level;
            Delta = delta;
            IsCoefficient = isCoefficient;
        }

        public override Config Apply(Config config)
        {
            if (config is LevelConfig levelConfig)
            {
                if(IsCoefficient)
                    levelConfig.Speed *= Delta;
                else 
                    levelConfig.Speed += Delta;
                return levelConfig;
            }
            return config;
        }
    }
}