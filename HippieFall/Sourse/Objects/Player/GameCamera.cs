using Godot;
using System;
using System.Drawing.Imaging;

namespace HippieFall.Game
{
    public class GameCamera : Godot.Camera
    {
        private Player _player;
        private float _speedProportion = 1 / 1.5f;
        private float yBackPosition = 8.8f;
        private float rotateXAngle = 0.001f;
        private float rotateZAngle = 0.001f;
        private float moveAngleX = 20 / 50f;
        private float moveAngleZ = 30 / 50f;
        private float maxAngleX = 5;
        private float maxAngleZ = 7.5f;
        private float distanceKoef = 0.01f;
        
        public override void _Ready()
        {
            HippieFallUtilities.ConnectFeedbackAfterGameReadiness(this);
        }

        private void Init()
        {
            _player = HippieFallUtilities.Game.Player;
        }

        public override void _PhysicsProcess(float delta)
        {
            MoveToPlayer(delta);
            RotateCameraToPlayerDirection(delta);
        }
        
        private void MoveToPlayer(float delta)
        {
            Vector2 button = _player.PlayerMovementController.Joystick.GetButtonPosition();

            if(distanceKoef - 0.5f*button.Length()/64 > 0)
                distanceKoef -= delta;
            else distanceKoef += delta/2;
                
            distanceKoef = Mathf.Clamp(distanceKoef, 0.01f, 0.5f);
            Translation = new Vector3(
                _player.Translation.x * _speedProportion,
                _player.Translation.y + distanceKoef + yBackPosition - _player.Translation.y,
                _player.Translation.z * _speedProportion);
        }


        private void RotateCameraToPlayerDirection(float delta)
        {
            Vector2 move = _player.PlayerMovementController.Joystick.GetValue();
            Vector2 button = _player.PlayerMovementController.Joystick.GetButtonPosition();
            move.y *= -1;
            if (move.x > 0) moveAngleZ = 20f;
            if (move.x < 0) moveAngleZ = -20f;
            if (move.y > 0) moveAngleX = 20f;
            if (move.y < 0) moveAngleX = -20f;
            
            moveAngleX *= Mathf.Abs(move.y) * button.Length() / 64 * delta;
            moveAngleZ *= Mathf.Abs(move.x) * button.Length() / 64 * delta;
            if (rotateXAngle + moveAngleX < maxAngleX && rotateXAngle + moveAngleX > -maxAngleX)
                rotateXAngle += moveAngleX;
            else
            {
                moveAngleX *= 0;
            }

            if (rotateZAngle + moveAngleZ < maxAngleZ && rotateZAngle + moveAngleZ > -maxAngleZ)
                rotateZAngle += moveAngleZ;
            else
            {
                moveAngleZ *= 0;
            }
            
            RotationDegrees = new Vector3(rotateXAngle + (-90), 0, rotateZAngle);
        }
    }
}