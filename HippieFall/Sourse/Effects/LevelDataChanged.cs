using Global.Data;
using Global.Data.EffectSystem;
using HippieFall.Game;
using HippieFall.Items;

namespace HippieFall.Effects
{
    public class LevelDataChanged:Effect
    {
        private LevelConfig Config;
        public LevelDataChanged(Config config)
        {
            Config = (LevelConfig)config;
            Target = EffectsTarget.Player;
        }
        public override Config Apply(Config config)
        {
            PlayerConfig playerConfig = (PlayerConfig)config;
            ApplyEffect(playerConfig.ItemConfig);
            ApplyEffect(playerConfig.MovementConfig);
            return playerConfig;
        }

        private void ApplyEffect(ItemConfig itemConfig)
        {
            itemConfig.MagnetConfig.SpeedForceCorrection = Config.Speed;
        }

        private void ApplyEffect(MovementConfig movementConfig)
        {
            movementConfig.Speed+=0.05f;
        }
    }
}