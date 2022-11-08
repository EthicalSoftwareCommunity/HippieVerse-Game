using Godot;

namespace Global.Data.CharacterSystem
{
	public class CharacterInterface:Control
	{
		[Export] public NodePath AbilityButtonsControllerPath;
		
		public AbilityButtonsController AbilityButtonsController { get; private set; }

		public override void _Ready()
		{
			AbilityButtonsController = GetNode<AbilityButtonsController>(AbilityButtonsControllerPath);
		}
	}
}
