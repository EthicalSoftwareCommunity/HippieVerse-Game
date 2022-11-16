using System;
using System.Timers;
using Godot;

namespace Global.Data.EffectSystem
{
    public class DynamicEffect : Effect
    {
        public event Action<Effect> OnRemoveEffect;
        public float EffectDuration
        {
            get => _effectDuration;
            set
            {
                _effectDuration = value;
                timer = new System.Timers.Timer(value*1000);
                timer.Elapsed += RemoveEffect;
                timer.AutoReset = false;
            }
        }
        
        private float _effectDuration;

        public Effect Effect { get; private set; }
        private System.Timers.Timer timer;
        
        public DynamicEffect(Effect effect, float duration)
        {
            Target = effect.Target;
            Effect = effect;
            EffectDuration = duration;
        }
        
        public override Config Apply(Config config)
        {
            timer.Start();
            Effect.Apply(config);
            return config;
        }

        public void RemoveEffect()
        {
            OnRemoveEffect?.Invoke(this);
        }
            
        private void RemoveEffect(object sender, ElapsedEventArgs e)
        {
            RemoveEffect();
        }
    }
}