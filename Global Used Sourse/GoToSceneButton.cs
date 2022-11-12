using Global;
using Global.Constants;
using Godot;

namespace HippieUniverse.HippieUniverse.Sourse.ChooseCharacter;

public class GoToSceneButton : Button
{
    [Export] public C_ScenesPath.Scenes scene;
    public override void _Ready()
    {
        Connect("pressed", this, nameof(GoToScene));
    }

    private void GoToScene()
    {
        Utilities.GoToScene(this, C_ScenesPath.GetScenePathByEnum(scene));
    }
}