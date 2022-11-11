using Global.Data.CharacterSystem;
using Global.Data.EffectSystem;
using HippieFall.Effects;

namespace HippieFall.Characters
{
    public class SlowObstacles : ActiveAbility
    {
        public SlowObstacles()
        {
            Effects.Add(new DynamicEffect(
                new ChangeSpeedObstacles(1/8f), 5
                ));
        }
    }
}
