using Godot;
using System;

namespace HippieFall.Game
{
    public class GameCamera : Godot.Camera
    {
        private Player _player;
        private float _speedProportion = 1/2f;
        public override void _Ready()
        {
            GetNode("/root").GetChild(0).Connect(nameof(GameController.GameIsReady), this, nameof(Init));
        }

        private void Init(GameController game)
        {
            _player = game.Player;
        }

        public override void _PhysicsProcess(float delta)
        {
            MoveToPlayer();
        }

        private void MoveToPlayer()
        {
            Translation = new Vector3(_player.Translation.x*_speedProportion, Translation.y, _player.Translation.z*_speedProportion);
        }
    }
}
