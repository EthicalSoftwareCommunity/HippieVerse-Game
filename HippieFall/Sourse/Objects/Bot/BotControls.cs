using Godot;
using System;
using Global.Data;
using HippieFall.Game;

namespace HippieFall.Bot
{
    public class BotControls : Node
    {
        [Export] private NodePath _playerMovementPath;

        public BotConfig BotConfig { get; set; }
        public BotMovementController BotMovementController { get; private set; }
        public BotEffectController BotEffectController { get; set; }

        public override void _Ready()
        {
            //BotMovementController = GetNode<BotMovementController>(_playerMovementPath);
            //BotConfig = new BotConfig(); 
        }
    }
}