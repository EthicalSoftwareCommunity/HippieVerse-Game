using System;
using System.Collections.Generic;
using Global.Data.EffectSystem;
using Godot;

namespace Global.Data.EffectSystem
{
    public abstract class NamedEffect : Node
    {
        public List<Effect> Effects = new List<Effect>();
        public event Action<NamedEffect> OnNamedEffectRemoved;
        public void RemoveDynamicsEffects()
        {
            foreach (var effect in Effects)
            {
                if(effect is DynamicEffect dynamicEffect)
                    dynamicEffect.RemoveEffect();
            }
            OnNamedEffectRemoved?.Invoke(this);
        }
    }
}