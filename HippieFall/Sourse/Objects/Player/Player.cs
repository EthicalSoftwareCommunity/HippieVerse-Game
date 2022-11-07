using Global.Constants;
using Godot;
using Global.Data;
using HippieFall.Game;
using HippieFall.Items;
using HippieFall.Tunnels;
using HippieFall.Characters;

namespace HippieFall
{
	public class Player : KinematicBody
	{
		[Export] private NodePath _playerControlsPath;
		[Export] private NodePath _characterPath;
		[Export] private NodePath _re_spawnOverlayPath;
		[Export] private bool _isPaused; 
		
		public Character Character { get; private set; }
		public PlayerControls PlayerControls { get; private set; }
		private Control _re_spawnOverlay;
		

		public PlayerRewardController PlayerRewardController => PlayerControls.PlayerCollectableController.PlayerRewardController;
		public PlayerCollectableController PlayerCollectableController => PlayerControls.PlayerCollectableController;
		public PlayerItemsController PlayerItemsController => PlayerControls.PlayerItemsController;
		public PlayerEffectController PlayerEffectController => PlayerControls.PlayerEffectController;
		public PlayerMovementController PlayerMovementController => PlayerControls.PlayerMovementController;

		public override void _Ready()
		{	
			_re_spawnOverlay = GetNode<Control>(_re_spawnOverlayPath);
			PlayerControls = GetNode<PlayerControls>(_playerControlsPath);
			Character = GetNode<Character>(_characterPath);
			GetNode("/root").GetChild(0).Connect(nameof(GameController.GameIsReady), this, nameof(Init));
			
		}
		
		private void Init(GameController game)
		{
			game.Level.ConfigChanged += UpdateDataByLevelConfig;
		}
		
		private void OnAreaEntered(Area area)
		{
			if (area.GetOwnerOrNull<Obstacle>() is Obstacle obstacle)
			{
				if (obstacle.ObstacleType == Obstacle.ObstacleTypes.Controller) 
					return;
				_re_spawnOverlay.Visible = _isPaused;
				GetTree().Paused = _isPaused;
				GD.Print(area.GetParent().Name + "Lose");
				//PlayerRewardController.LoseGame();
				//GetTree().ChangeScene(C_ScenesPath.MAIN_SPHERE);
			}
		}

		private void UpdateDataByLevelConfig(Config levelConfig)
		{
			PlayerEffectController.BonusesEffectController.AddEffect(new LevelDataChanged(levelConfig));
		}
	}
}
