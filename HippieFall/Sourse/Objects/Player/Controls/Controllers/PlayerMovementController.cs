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
        public event Action<List<Effect>> OnMovementEffectAdded; 
        private Player _player;
        private Joystic _joystick;
        private Vector3 _nextMove;
        private Vector3 _move;
        private IncreaseLevelSpeedByTapArea _holdTapArea;

        private MovementConfig _config = new MovementConfig();
        public override void _Ready()
        {
            GetTree().Root.GetChild(0).Connect(nameof(GameController.GameIsReady), this, nameof(Init));
        }
        
        private void AddMovementEffect(List<Effect> obj)
        {
            OnMovementEffectAdded?.Invoke(obj);
        }

        public void Init(GameController game)
        {
            _joystick = game.GameInterface.Joystic;
            _player = game.Player;
            _holdTapArea = game.GameInterface.IncreaseLevelSpeedByTapArea;
            _holdTapArea.OnEffectAdded += AddMovementEffect;
        }

        public override void _PhysicsProcess(float delta)
        {
            _move.x = _joystick.GetButtonPosition().x;
            _move.z = _joystick.GetButtonPosition().y;

            Vector2 move2d = new Vector2(_move.x, _move.y);
            if (move2d.Length() > 0)
            {
                _move *= +_config.Speed * delta * GetMovementCoefficient(move2d.Normalized().Length());
                if (CheckMoveOnBorderRadiusOut())
                    _player.Translation += _move;
                else
                {
                    _nextMove = _player.Translation;
                    if (Mathf.Abs(_move.x) > Mathf.Abs(_move.z))
                    {
                        _nextMove.x = Mathf.Clamp(_nextMove.x + _move.x/5, -_config.Radius, _config.Radius);
                        _nextMove.z = Mathf.Sqrt(_config.Radius * _config.Radius - _nextMove.x * _nextMove.x) *
                                      Mathf.Sign(_nextMove.z);
                    }
                    else
                    {
                        _nextMove.z = Mathf.Clamp(_nextMove.z + _move.z/5, -_config.Radius, _config.Radius);
                        _nextMove.x = Mathf.Sqrt(_config.Radius * _config.Radius - _nextMove.z * _nextMove.z) *
                                      Mathf.Sign(_nextMove.x);
                    }

                    _player.Translation = _nextMove;
                }
            }
        }

        private float GetMovementCoefficient(float x)
        {
            return Mathf.Log(x*5) / 200;
        }

        private bool CheckMoveOnBorderRadiusOut()
        {
            _nextMove = _player.Translation + _move;
            return _config.Radius * _config.Radius - _nextMove.x * _nextMove.x > _nextMove.z * _nextMove.z;
        }

        public void ChangeConfigData(Config config)
        {
            _config = ((PlayerConfig)config).MovementConfig;
        }
    }
}