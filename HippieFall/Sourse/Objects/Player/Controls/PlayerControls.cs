using Global.Data;
using Global.Data.EffectSystem;
using Godot;
using HippieFall.Game;
using HippieFall.Items;

namespace HippieFall
{
	public class PlayerControls : Node, IEffectable
	{
		[Export] private NodePath _playerMovementPath;
		[Export] private NodePath _playerCollectableControllerPath;
		[Export] private NodePath _playerItemsControllerPath;
		
		public PlayerConfig PlayerConfig { get; set; }
		public PlayerItemsController PlayerItemsController { get; private set; }
		public PlayerCollectableController PlayerCollectableController { get; private set; }
		public PlayerMovementController PlayerMovementController { get; private set; }
		public PlayerEffectController PlayerEffectController { get; set; }

		public override void _Ready()
		{
			PlayerCollectableController = GetNode<PlayerCollectableController>(_playerCollectableControllerPath);
			PlayerMovementController = GetNode<PlayerMovementController>(_playerMovementPath);
			PlayerItemsController = GetNode<PlayerItemsController>(_playerItemsControllerPath);
			
			PlayerConfig = new PlayerConfig();
			PlayerEffectController = new PlayerEffectController();
			HippieFallUtilities.ConnectFeedbackAfterGameReadiness(this);
		}

		private void Init()
		{
			PlayerEffectController.Init(this, PlayerConfig);
		}
		public void ChangeConfigData(Config config)
		{
			PlayerMovementController.ChangeConfigData(config);
			PlayerItemsController.ChangeConfigData(config);
		}
	}
}
