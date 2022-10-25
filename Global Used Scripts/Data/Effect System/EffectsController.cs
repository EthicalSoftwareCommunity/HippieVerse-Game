using System;
using System.Collections.Generic;
using Godot;

namespace Global.Data.EffectSystem
{
    public class EffectsController : Node
    {
        public event Action DynamicEffectAdded;
        public event Action<Effect> ConstantEffectAdded;
        public List<Effect> Effects { get; private set; }
        
        public Effect.EffectsTarget EffectTarget { get; private set; }
        
        public EffectsController(Effect.EffectsTarget effectTarget)
        {
            EffectTarget = effectTarget;
            Effects = new List<Effect>();
        }

        public void AddEffect(Effect effect)
        {
            if (effect.Target == EffectTarget)
            {
                if (effect is DynamicEffect dynamic)
                {
                    Effects.Add(dynamic);
                    dynamic.OnRemoveEffect += RemoveEffect;
                    DynamicEffectAdded?.Invoke();
                    return;
                }
                ConstantEffectAdded?.Invoke(effect);
            }
        }
        public void RemoveEffect(Effect effect)
        {
            Effects.Remove(effect);
            DynamicEffectAdded?.Invoke();
        }
        
        public Config ApplyEffectsOnConfig(Config config)
        {
            Config _config = config;
            foreach (var effect in Effects)
            {
                _config = effect.Apply(config);
            }

            return _config;
        }
    }
}