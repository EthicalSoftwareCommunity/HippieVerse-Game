using Global.Data.CharacterSystem;
using Global.Data.EffectSystem;
using HippieFall.Effects;

namespace HippieFall.Characters
{
    public class DoubleRewardIncreaseAbility : PassiveAbility
    {
        public override void _Ready()
        {
            Effects.Add(new DynamicEffect(new RewardIncrease(2),20));
        }
    }
}