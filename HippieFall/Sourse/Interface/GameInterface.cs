using System.Drawing.Printing;
using Global.Data.CharacterSystem;
using Godot;

namespace HippieFall
{
	public class GameInterface : Control
	{
		public Label GameScore;
		public Joystic Joystic;
		public RewardRenderer RewardRenderer;
		public CharacterInterface CharacterInterface;
		public Control BottomUI;
		public Control TopUI;

		public override void _Ready()
		{
			BottomUI = GetNode<Control>("BottomUI");
			TopUI = GetNode<Control>("TopUI");
			Joystic = BottomUI.GetNode<Joystic>("Joystic/TouchScreenButton");
			RewardRenderer = TopUI.GetNode<RewardRenderer>("HBoxContainer/RewardRender");
			GameScore = TopUI.GetNode<Label>("HBoxContainer/Label");
			GameScore.Text = "0";
		}

		private void OnBottomUIChildEnteredTree(Node node)
		{
			if (node is CharacterInterface characterInterface)
				CharacterInterface = characterInterface;
		}
	}
}
