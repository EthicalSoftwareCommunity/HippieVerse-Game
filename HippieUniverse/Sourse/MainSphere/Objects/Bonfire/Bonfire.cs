using Global;
using Global.Constants;

namespace HappyUniverse
{
	public class Bonfire : InteractionController
	{
		public override void HoldTouchInteract()
		{
			base.HoldTouchInteract();
			GetTree().ChangeScene(C_ScenesPath.CHOOSE_CHARACTER);
		}
	}
}