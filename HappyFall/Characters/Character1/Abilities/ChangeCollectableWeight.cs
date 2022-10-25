using Global.Data.CharacterSystem;
using Global.Data.EffectSystem;
using HippieFall.Collectables;

namespace HippieFall.Characters
{
    public class ChangeCollectableWeight : PassiveAbility
    {
        public ChangeCollectableWeight()
        {
            Effects.Add(new DynamicEffect(
                new Tunnels.Effects.ChangeCollectableWeight(
                    new CollectableCoinConfig()
                    {
                        SpawnWeight = 100,
                    }, true),20f));   
        }
    }

}
