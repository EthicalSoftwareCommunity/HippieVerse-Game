using System;
using System.Collections.Generic;
using Global.Data;
using Global.Data.EffectSystem;
using Godot;
using HippieFall.Effects;
using HippieFall.Game;

namespace HippieFall
{
    public class PlayerMovementController : Node, IEffectable
    {
        public event Action<NamedEffect> OnMovementEffectAdded; 
        public Joystic Joystick { get; private set; }
        private Player _player;
        private Vector3 _nextMove;
        private Vector3 _move;
        private IncreaseLevelSpeedByTapArea _holdTapArea;

        private MovementConfig _config = new MovementConfig();
        public override void _Ready()
        {
            HippieFallUtilities.ConnectFeedbackAfterGameReadiness(this);
        }
        
        private void AddMovementEffect(NamedEffect namedEffect)
        {
            OnMovementEffectAdded?.Invoke(namedEffect);
        }

        public void Init()
        {
            Joystick = HippieFallUtilities.Game.GameInterface.Joystic;
            _player = HippieFallUtilities.Game.Player;
            _holdTapArea = HippieFallUtilities.Game.GameInterface.IncreaseLevelSpeedByTapArea;
            _holdTapArea.OnEffectAdded += AddMovementEffect;
        }

        public override void _PhysicsProcess(float delta)
        {
            _move.x = Joystick.GetButtonPosition().x;
            _move.z = Joystick.GetButtonPosition().y;
            Vector2 move2d = new Vector2(_move.x, _move.z);
            if (move2d.Length() > 0)
            {
                _move *= +_config.Speed * delta * GetMovementCoefficient(move2d.Normalized().Length());
                _player.MoveAndCollide(_move);
            }
        }

        private float GetMovementCoefficient(float x)
        {
            return Mathf.Log(x*5) / 200;
        }
        
        public void ChangeConfigData(Config config)
        {
            _config = ((PlayerConfig)config).MovementConfig;
        }
    }
}