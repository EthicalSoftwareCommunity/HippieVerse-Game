using Godot;

namespace HippieUniverse
{
	class GameInterface:Control
	{
		public RewardRenderer RewardRenderer;

		public override void _Ready()
		{
			RewardRenderer = GetNode<RewardRenderer>("TopUI/HBoxContainer/RewardRender");
		}
	}
}