using Global.Data.CharacterSystem;
using HippieFall.Tunnels;

namespace HippieFall.Characters
{
    public class DoubleRewardIncreaseAbility : PassiveAbility
    {
        public override void _Ready()
        {
            Effects.Add(new RewardIncrease(2));
        }
    }
}