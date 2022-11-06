using Godot;

namespace HippieFall.Game
{
	public class GameController : Spatial
	{
		[Signal] public delegate void GameIsReady(GameController game);
		public GameInterface GameInterface { get; private set; }
		public GameEffectController GameEffectController { get; private set; }
		public Player Player { get; private set; }
		public LevelController Level { get; private set; }

		public override void _Ready()
		{
			GameInterface = GetNode<GameInterface>("Interface");
			Player = GetNode<Player>("Player");
			Level = GetNode<LevelController>("Level");
			GameEffectController = GetNode<GameEffectController>("GameEffectController");
			EmitSignal(nameof(GameIsReady), this);
		}
	}
}
