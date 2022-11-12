using Global;
using Global.Constants;
using Godot;

namespace Global.GameSystem
{
	public class GoToSceneByObject : InteractionController
	{
		[Export] public C_ScenesPath.Scenes scene;
		[Export] public ActivateType activateType;

		public enum ActivateType
		{
			HoldTouch,
			Touch
		}

		public override void TouchInteract()
		{
			if(activateType == ActivateType.Touch) 
				GetTree().ChangeScene(C_ScenesPath.GetScenePathByEnum(scene));
		}
		public override void HoldTouchInteract()
		{
			if(activateType == ActivateType.HoldTouch) 
				GetTree().ChangeScene(C_ScenesPath.GetScenePathByEnum(scene));
		}

		
	}
}
