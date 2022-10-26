using Godot;

namespace Global.Data.CharacterSystem
{
    public class AbilityButton : Button
    {
        public Ability Ability;
        [Export] public NodePath AbilityPath;

        public override void _Ready()
        {
            Ability = GetNode<Ability>(AbilityPath);
        }
    }
}