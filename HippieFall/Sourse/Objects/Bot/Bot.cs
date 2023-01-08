using Godot;
using System;
using HippieFall;
using HippieFall.Characters;
using HippieFall.Effects;
using HippieFall.Game;
using HippieFall.Items;
using HippieFall.Tunnels;

namespace HippieFall.Bot
{
    public class Bot : KinematicBody
    {
        /*[Export] private NodePath _playerControlsPath;
        [Export] private NodePath _characterPath;

        public event Action OnPlayerEndedGame; 
        public Character Character { get; private set; }
        public PlayerControls PlayerControls { get; private set; }
		
        public PlayerRewardController PlayerRewardController => PlayerControls.PlayerCollectableController.PlayerRewardController;
        public PlayerCollectableController PlayerCollectableController => PlayerControls.PlayerCollectableController;
        public PlayerItemsController PlayerItemsController => PlayerControls.PlayerItemsController;
        public PlayerEffectController PlayerEffectController => PlayerControls.PlayerEffectController;
        public PlayerMovementController PlayerMovementController => PlayerControls.PlayerMovementController;*/
        
        [Export] private NodePath _botMarkersControllerPath;
        [Export] private NodePath _botMovementControllerPath;
        
        public BotMarkersController BotMarkersController { get; private set; }
        public BotMovementController BotMovementController { get; private set; }
        public override void _Ready()
        {
            BotMarkersController = GetNode<BotMarkersController>(_botMarkersControllerPath);
            BotMovementController = GetNode<BotMovementController>(_botMovementControllerPath);
            HippieFallUtilities.ConnectFeedbackAfterGameReadiness(this);
        }
        private void Init()
        {
            
        }
		
     
    }
}

