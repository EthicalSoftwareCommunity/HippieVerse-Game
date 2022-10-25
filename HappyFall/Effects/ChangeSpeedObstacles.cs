using Global.Data;
using Global.Data.EffectSystem;

namespace HippieFall.Tunnels
{
    public class ChangeSpeedObstacles:Effect
    {
        private readonly float _coefficient;
        public ChangeSpeedObstacles(float coefficient)
        {
            Target = EffectsTarget.Obstacles;
            _coefficient = coefficient;
        }

        public override Config Apply(Config config)
        {
            if (config is FanConfig fanConfig)
                return ApplyEffect(fanConfig);
            if (config is SawConfig sawConfig)
                return ApplyEffect(sawConfig);
            return config;
        }

        private FanConfig ApplyEffect(FanConfig config)
        {
            config.RotationSpeed *= _coefficient;
            return config;
        }
        private SawConfig ApplyEffect(SawConfig config)
        {
            config.Speed *= _coefficient;
            return config;
        }
    }
}