using Global.Data;
using Global.Data.EffectSystem;
using HippieFall.Game;

namespace HippieFall.Tunnels
{
    public class ChangeLevelSpeed:Effect
    {
        private readonly float _delta;
        public ChangeLevelSpeed(float delta = 0.1f)
        {
            Target = EffectsTarget.Level;
            _delta = delta;
        }

        public override Config Apply(Config config)
        {
            if (config is LevelConfig levelConfig)
            {
                levelConfig.Speed += _delta;
                return levelConfig;
            }
            return config;
        }
    }
}