using System;
using Global;
using Godot;

namespace HippieFall.Tunnels
{
    internal class MovingLaser : Obstacle
    {
        [Export] private float _radius;
        [Export] private float _rotateAngle = 90;
        [Export] private float _rotateSpeed = 300;
        
        public float RotateSpeed => _rotateSpeed;
        
        private Spatial _center;
        private float _currentRotateAngle;
        private float _halfRotateAngle;
        private float _leftBorderAngle;
        private float _rightBorderAngle;
        private int _leftRightCoef = 1;

        public override void _Ready()
        {
            _center = GetNode<Spatial>("../Center");
            _halfRotateAngle = _rotateAngle / 2;
            SpawnPlatform();
        }
        private void SpawnPlatform()
        {
            float spawnPointX = Utilities.GetRandomNumberFloat(-_radius, _radius);
            
            int verticalPositionCoefficient = Utilities.GetRandomNumberInt(-1, 1, Utilities.Parameter.WITH_OUT_ZERO);
            float spawnPointZ = Mathf.Sqrt(_radius * _radius - spawnPointX * spawnPointX) * verticalPositionCoefficient;

            Translation = new Vector3(spawnPointX, 0, spawnPointZ);

            Vector3 direction = Translation.DirectionTo(_center.Translation);
            LookAt(direction, Vector3.Up);
            RotateY(Mathf.Deg2Rad(-90));
            _rightBorderAngle = Mathf.Rad2Deg(GlobalRotation.y) + _halfRotateAngle;
            _leftBorderAngle = Mathf.Rad2Deg(GlobalRotation.y) - _halfRotateAngle;
        }

        public override void _PhysicsProcess(float delta)
        {
            RotatePlatform(delta);
        }

        private void RotatePlatform(float delta)
        {
            _currentRotateAngle = Mathf.Rad2Deg(GlobalRotation.y);
            _currentRotateAngle = Mathf.Clamp(_currentRotateAngle + _rotateSpeed * _leftRightCoef * delta,
                _leftBorderAngle,
                _rightBorderAngle);
            if (!(Math.Abs(_currentRotateAngle - _rightBorderAngle) < 0.1f)
                && Math.Abs(_currentRotateAngle - _leftBorderAngle) > 0.1f)
                _leftRightCoef *= -1;

            RotateY(Mathf.Deg2Rad(_rotateSpeed * _leftRightCoef * delta));
        }
    }
}