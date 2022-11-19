using Global;
using Godot;
namespace HippieFall.Game;

public static class HippieFallUtilities
{
	public static GameController Game { get; set; }
	public static void PauseGame()
	{
		Utilities.PauseNode(Game.Player, true);
		Utilities.PauseNode(Game.Level);
	}
	public static void ResumeGame()
	{
		Utilities.ResumeNode(Game.Player, true);
		Utilities.ResumeNode(Game.Level);
	}
	public static void ReplayGame(Node node, string path)
	{
		Utilities.GoToScene(node,path);
	}

	public static void ConnectFeedbackAfterGameReadiness(Node node, string methodName = "Init")
	{
		Game.Connect("ready", node, methodName);
	}
}

