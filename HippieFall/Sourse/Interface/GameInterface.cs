using System.Drawing.Printing;
using Global.Data.CharacterSystem;
using Godot;
using HippieFall.Game;

namespace HippieFall
{
	public class GameInterface : Control
	{
		[Export] private NodePath GameScorePath;
		[Export] private NodePath JoysticPath;
		[Export] private NodePath IncreaseLevelSpeedByTapAreaPath;
		[Export] private NodePath RewardRendererPath;
		[Export] private NodePath PauseButtonPath;
		[Export] private NodePath BottomUIPath;
		[Export] private NodePath TopUIPath;
		
		public Label GameScore;
		public Joystic Joystic;
		public IncreaseLevelSpeedByTapArea IncreaseLevelSpeedByTapArea;
		public RewardRenderer RewardRenderer;
		public CharacterInterface CharacterInterface;
		public PauseButton PauseButton;
		public Control BottomUI;
		public Control TopUI;

		public override void _Ready()
		{
			BottomUI = GetNode<Control>(BottomUIPath);
			TopUI = GetNode<Control>(TopUIPath);
			IncreaseLevelSpeedByTapArea = GetNode<IncreaseLevelSpeedByTapArea>(IncreaseLevelSpeedByTapAreaPath);
			Joystic = GetNode<Joystic>(JoysticPath);
			RewardRenderer = GetNode<RewardRenderer>(RewardRendererPath);
			PauseButton = GetNode<PauseButton>(PauseButtonPath);
			GameScore = GetNode<Label>(GameScorePath);
			GameScore.Text = "0";
		}

		private void OnBottomUIChildEnteredTree(Node node)
		{
			if (node is CharacterInterface characterInterface)
				CharacterInterface = characterInterface;
		}
	}
}
