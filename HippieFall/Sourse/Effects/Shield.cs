using Global.Data;
using Global.Data.EffectSystem;
using HippieFall.Items;

namespace HippieFall.Effects
{
    public class Shield : Effect
    {
        private ShieldConfig _config;

        public Shield()
        {
            Target = EffectsTarget.Player;
            _config = new ShieldConfig();
        }

        public override Config Apply(Config config)
        {
            if (config is PlayerConfig playerConfig)
            {
                playerConfig.ItemConfig.ShieldConfig.IsShieldActivated = true;
                return playerConfig;
            }

            return config;
        }
    }
}