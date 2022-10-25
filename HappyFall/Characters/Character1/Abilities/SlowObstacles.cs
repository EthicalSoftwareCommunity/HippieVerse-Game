using Godot;
using System;
using Global.Data.CharacterSystem;
using Global.Data.EffectSystem;
using HippieFall.Tunnels;

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
