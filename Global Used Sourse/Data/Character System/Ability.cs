using System;
using System.Collections.Generic;
using Godot;
using Global.Data.EffectSystem;

namespace Global.Data.CharacterSystem
{
    public abstract class Ability : Node
    {
        public event Action<Ability> OnAbilityActivated; 
        public List<Effect> Effects = new List<Effect>();
        
        public virtual void Activate()
        {
            OnAbilityActivated?.Invoke(this);
        }
    }
}