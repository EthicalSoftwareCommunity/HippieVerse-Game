using Global.Data;
using Global.Data.EffectSystem;
using HippieFall.Collectables;

namespace HippieFall.Characters
{
    public class RewardIncrease:Effect
    {
        private float _coefficient;
        public RewardIncrease(float coefficient = 2)
        {
            Target = EffectsTarget.Collectable;
            _coefficient = coefficient;
        }

        public override Config Apply(Config config)
        {
            if (config is CollectableChestConfig chest)
                ApplyEffect(chest);
            if (config is CollectableCoinConfig coin)
                ApplyEffect(coin);
            if (config is CollectableGemcoinConfig gemcoin)
                ApplyEffect(gemcoin);
            return config;
        }

        private void ApplyEffect(CollectableChestConfig chest)
        {
            chest.Value = (int)(chest.Value * _coefficient);
        }

        private void ApplyEffect(CollectableCoinConfig coin)
        {
            
            coin.Value = (int)(coin.Value*_coefficient);
        }

        private void ApplyEffect(CollectableGemcoinConfig gemcoin)
        {
            gemcoin.Value = (int)(gemcoin.Value*_coefficient);
        }
    }
}