using Global.Data.EffectSystem;

namespace Global.GameSystem;

public interface IEffectableController
{
    void ApplyDynamicEffects();

    public void ApplyConstantEffect(Effect effect);
}