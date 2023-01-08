using Godot;

namespace HippieFall.Game
{
	public class GameController : Spatial
	{
		[Signal] public delegate void GameIsReady(GameController game);
		[Export] private NodePath _botPath;
		
		public bool IsGameIsReady = false;
		public GameInterface GameInterface { get; private set; }
		public GameEffectController GameEffectController { get; private set; }
		public GameCamera GameCamera { get; private set; }
		public Player Player { get; private set; }
		public Bot.Bot Bot { get; private set; }
		public LevelController Level { get; private set; }

		public GameController()
		{
			HippieFallUtilities.Game = this;
		}
		public override void _Ready()
		{
			GameInterface = GetNode<GameInterface>("Interface");
			Player = GetNode<Player>("Player");
			Bot = GetNode<Bot.Bot>(_botPath);
			Level = GetNode<LevelController>("Level");
			GameCamera = GetNode<GameCamera>("GameCamera");
			GameEffectController = GetNode<GameEffectController>("GameEffectController");
			EmitSignal(nameof(GameIsReady), this);
			IsGameIsReady = true;
		}
		public override void _Notification(int what)
		{
			if (what == MainLoop.NotificationAppPaused || what == MainLoop.NotificationWmFocusOut)
			{
				GameInterface.PauseButton.SetPause();
			}
		}
	}
}
