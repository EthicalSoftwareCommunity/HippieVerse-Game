using Godot;
using System;
using System.Drawing.Imaging;

namespace HippieFall.Game
{
    public class GameCamera : Godot.Camera
    {
        internal class PreviousCurrentValue
        {
            private float _currentValue;

            public float CurrentValue
            {
                get => _currentValue;
                set
                {
                    PreviousValue = _currentValue;
                    _currentValue = value;
                }
            }

            public float PreviousValue { get; set; }
            public float MoveCoefficient { get; set; }

            public PreviousCurrentValue(float currentValue, float previousValue)
            {
                CurrentValue = currentValue;
                PreviousValue = previousValue;
            }

            public bool IsChangedIncreaseDirection
            {
                get
                {
                    if (CurrentValue > PreviousValue) return false;
                    return true;
                }
            }
        }

        private Player _player;
        private float _speedProportion = 1 / 1.5f;
        private float yBackPosition = 8.8f;
        private float rotateXAngle = 0.001f;
        private float rotateZAngle = 0.001f;
        private float moveAngleX = 20 / 50f;
        private float moveAngleZ = 30 / 50f;
        private float maxAngleX = 5;
        private float maxAngleZ = 7.5f;
        private float timeToRotate = 1000;
        private float distanceKoef = 0.01f;
        private bool _isMovingToZeroAngle;

        private bool IsMovingToZeroAngle
        {
            get => _isMovingToZeroAngle;
            set
            {
                _isMovingToZeroAngle = value;
                if (_isMovingToZeroAngle)
                {
                    CalculateMoveAnglesToZero();
                }
            }
        }

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

        private void CalculateMoveAnglesToZero()
        {
            moveAngleX = timeToRotate / rotateXAngle * Mathf.Sign(-rotateXAngle);
            moveAngleZ = timeToRotate / rotateZAngle * Mathf.Sign(-rotateZAngle);
        }

        private void CalculateMoveAnglesToX(float moveX)
        {
            moveAngleX = timeToRotate / (rotateXAngle - maxAngleX * Mathf.Sign(-rotateXAngle)) *
                         Mathf.Sign(-rotateXAngle);
        }

        private void CalculateMoveAnglesToZ(float moveY)
        {
            moveAngleZ = timeToRotate / (rotateZAngle - maxAngleZ * Mathf.Sign(-rotateZAngle)) *
                         Mathf.Sign(-rotateZAngle);
        }

        private void MoveToPlayer(float delta)
        {
            Vector2 button = _player.PlayerMovementController.Joystick.GetButtonPosition();

            if(distanceKoef - 0.4f*button.Length()/64 > 0)
                distanceKoef -= delta;
            else distanceKoef += delta/2;
                
            distanceKoef = Mathf.Clamp(distanceKoef, 0.01f, 0.4f);
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

            GD.Print(move);
            GD.Print(button.Length());
            /*GD.Print("X: " + rotateXAngle);
            GD.Print("Z: " + rotateZAngle);*/
            /*if (IsMovingToZeroAngle)
            {
                if (rotateXAngle > 0 || -rotateXAngle < 0)
                {
                    rotateXAngle += moveAngleX;
                    RotateX(Mathf.Deg2Rad(moveAngleX));

                if (Math.Abs(Mathf.Abs(rotateZAngle)) > 0.1f)
                    RotateZ(Mathf.Deg2Rad(moveAngleZ));
            }*/
            RotationDegrees = new Vector3(rotateXAngle + (-90), 0, rotateZAngle);
        }
    }
}