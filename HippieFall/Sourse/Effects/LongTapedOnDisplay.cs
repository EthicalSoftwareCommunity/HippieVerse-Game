using Global.Data.EffectSystem;

namespace HippieFall.Effects;

public class LongTapedOnDisplay : NamedEffect
{
    public LongTapedOnDisplay()
    {
         Effects.Add(new DynamicEffect(new ChangeLevelSpeed(1.5f, true), 9999));
         Effects.Add(new DynamicEffect(new IncreaseLevelSpeedCameraEffect(), 9999));
    }
}