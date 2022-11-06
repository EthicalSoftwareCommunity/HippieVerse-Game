using Global;
using Global.Constants;

namespace HappieUniverse
{
	public class HippieFallScene : InteractionController
	{
		public override void HoldTouchInteract()
		{
			base.HoldTouchInteract();
			GetTree().ChangeScene(C_ScenesPath.HIPPIE_FALL);
		}

	}
}
