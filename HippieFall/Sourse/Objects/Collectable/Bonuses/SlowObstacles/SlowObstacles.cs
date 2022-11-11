using Global.Data.EffectSystem;
using HippieFall.Effects;

namespace HippieFall.Collectables
{
    class SlowObstacles : Bonus
    {
        public SlowObstacles()
        {
            Config = new CollectableConfig()
            {
                SpawnWeight = 100f
            };
            Effects.Add(new DynamicEffect(new ChangeSpeedObstacles(0.5f), 5f));
        }
    }
}