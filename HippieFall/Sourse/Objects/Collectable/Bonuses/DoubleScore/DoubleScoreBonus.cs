using Global.Data.EffectSystem;
using HippieFall.Effects;

namespace HippieFall.Collectables
{
    public class DoubleScoreBonus:Bonus
    {
        public DoubleScoreBonus()
        {
            Config = new CollectableConfig()
            {
                SpawnWeight = 100f
            };
            Effects.Add(new DynamicEffect(new ScoreIncrease(2f),10f));
        }
    }
}

