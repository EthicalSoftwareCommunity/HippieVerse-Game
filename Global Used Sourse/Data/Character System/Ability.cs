using System;
using Global.Data.EffectSystem;

namespace Global.Data.CharacterSystem
{
    public abstract class Ability : NamedEffect
    {
        public event Action<Ability> OnAbilityActivated;

        public virtual void Activate()
        {
            OnAbilityActivated?.Invoke(this);
        }
    }
}