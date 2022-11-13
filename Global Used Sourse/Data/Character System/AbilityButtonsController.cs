using System;
using System.Collections.Generic;
using Godot;
using Array = Godot.Collections.Array;

namespace Global.Data.CharacterSystem
{
	public class AbilityButtonsController:Control
	{
		[Export] private List<NodePath> _abilityButtonsPaths;
		private List<AbilityButton> _abilityButtons;
		
		public event Action<Ability> OnAbilityActivated;

		public override void _Ready()
		{
			LoadFromScene();
			foreach (var abilityButton in _abilityButtons)
			{
				AddAbility(abilityButton);
			}
		}

		private void LoadFromScene()
		{
			_abilityButtons = new List<AbilityButton>();
			foreach (var path in _abilityButtonsPaths)
				_abilityButtons.Add(GetNode<AbilityButton>(path));
		}

		private void AddAbility(AbilityButton button)
		{
			button.Connect("gui_input", this, nameof(OnButtonPressed), new Array(button.GetIndex()));
		}

		private void OnButtonPressed(InputEvent @event,int idx)
		{
			if (@event is InputEventScreenTouch inputEvent)
			{
				if(inputEvent.Pressed)
				{
					OnAbilityActivated?.Invoke(_abilityButtons[idx].Ability);
					_abilityButtons[idx].Ability.Activate();
				}
			}
		}
	}
}
