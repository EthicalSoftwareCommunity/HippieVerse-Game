using Global.Data.EffectSystem;
using HippieFall.Effects;


namespace HippieFall.Collectables
{
    public class ShieldBonus : Bonus
    {
        public ShieldBonus()
        {
            Config = new CollectableConfig()
            {
                SpawnWeight = 30f
            };
            Effects.Add(new DynamicEffect(new Shield(), 20f));
        }
    }
}
