using System;
using System.Collections.Generic;
using Godot;
using Global.Data.EffectSystem;

namespace Global.Data.CharacterSystem
{
    public abstract class Character : Spatial
    {
        public event Action<Ability> OnCharacterEffectAdded;
        protected CharacterInterface _characterInterface;
        [Export] protected NodePath _characterInterfacePath;

        public override void _Ready()
        {
            _characterInterface = GetNode<CharacterInterface>(_characterInterfacePath);
            _characterInterface.AbilityButtonsController.OnAbilityActivated += HandleAbility;
         }
        private void HandleAbility(Ability ability)
        {
            OnCharacterEffectAdded?.Invoke(ability);
        }
    }
}