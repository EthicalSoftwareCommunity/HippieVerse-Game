using Godot;

namespace Global.Data.CharacterSystem
{
    public class CharacterInterface : Control
    {
        [Export] public NodePath AbilityButtonsControllerPath;
        [Export] public NodePath LeftPositionForAbilitiesButtonsPath;

        public AbilityButtonsController AbilityButtonsController { get; private set; }
        public Node2D LeftPositionForAbilitiesButtons;
        public override void _Ready()
        {
            AbilityButtonsController = GetNode<AbilityButtonsController>(AbilityButtonsControllerPath);
            LeftPositionForAbilitiesButtons = GetNode<Node2D>(LeftPositionForAbilitiesButtonsPath);
        }
    }
}
