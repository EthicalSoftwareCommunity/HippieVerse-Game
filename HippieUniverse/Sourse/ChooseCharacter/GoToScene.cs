using Godot;

namespace Global.GameSystem
{
    public class GoToScene : InteractionController
    {
        [Export] public PackedScene Scene;
        public override void HoldTouchInteract()
        {
            base.HoldTouchInteract();
            GetTree().ChangeScene(Scene.ResourcePath);
        }
    }
}
