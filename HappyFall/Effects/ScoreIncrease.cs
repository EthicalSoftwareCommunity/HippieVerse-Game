using Global.Data;
using Global.Data.EffectSystem;
using HippieFall.Game;

namespace HippieFall.Tunnels
{
    public class ScoreIncrease : Effect
    {
        private readonly float _coefficient;
        public ScoreIncrease(float coefficient = 2f)
        {
            Target = EffectsTarget.Level;
            _coefficient = coefficient;
        }
        public override Config Apply(Config config)
        {
            LevelConfig levelConfig = (LevelConfig)config;
            levelConfig.DeepIncrease *= _coefficient;
            return config;
        }
    }
}