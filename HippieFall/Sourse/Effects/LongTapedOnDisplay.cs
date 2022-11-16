using Global.Data.EffectSystem;

namespace HippieFall.Effects;

public class LongTapedOnDisplay : NamedEffect
{
    public LongTapedOnDisplay()
    {
         Effects.Add(new DynamicEffect(new ChangeLevelSpeed(2f, true), 9999));
    }

 
}