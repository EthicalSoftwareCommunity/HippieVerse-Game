using Global.Data;
using Global.Data.EffectSystem;
using Godot;
using HippieFall.Items;

namespace HippieFall
{
	public class PlayerControls : Node, IEffectable
	{
		[Export] private NodePath _playerMovementPath;
		[Export] private NodePath _playerCollectableControllerPath;
		[Export] private NodePath _playerEffectControllerPath;
		[Export] private NodePath _playerItemsControllerPath;
		
		public PlayerItemsController PlayerItemsController { get; private set; }
		public PlayerCollectableController PlayerCollectableController { get; private set; }
		public PlayerEffectController PlayerEffectController { get; private set; }
		public PlayerMovementController PlayerMovementController { get; private set; }

		public override void _Ready()
		{
			PlayerCollectableController = GetNode<PlayerCollectableController>(_playerCollectableControllerPath);
			PlayerMovementController = GetNode<PlayerMovementController>(_playerMovementPath);
			PlayerEffectController = GetNode<PlayerEffectController>(_playerEffectControllerPath);
			PlayerItemsController = GetNode<PlayerItemsController>(_playerItemsControllerPath);
		}
		
		public void ChangeConfigData(Config config)
		{
			PlayerMovementController.ChangeConfigData(config);
			PlayerItemsController.ChangeConfigData(config);
		}
	}
}
