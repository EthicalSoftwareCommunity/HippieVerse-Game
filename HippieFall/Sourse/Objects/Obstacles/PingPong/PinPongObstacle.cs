using System.Runtime.CompilerServices;
using Global;
using Godot;

namespace HippieFall.Tunnels
{
    class PinPongObstacle : Obstacle
    {
        [Export] private float _speed = 3;
        [Export] private float _radius = 2.5f;
        [Export] private float _directionOffset = 2;

        private Vector3 direction;
        private Vector3 targetPoint;
        private float nextX;
        private float nextZ;
        private float _circleNumber = 0f;
        private float x;
        private float z;
        private int xCoef = 1;
        private int zCoef = 1;

        public override void _Ready()
        {
            x = Mathf.Sin(_circleNumber * _speed) * _radius;
            z = Mathf.Cos(_circleNumber * _speed) * _radius;
            Translation = new Vector3(x, 0, z);
        }

        public override void _PhysicsProcess(float delta)
        {
            PinPongMoving(delta);
        }
        private void ChangeDirection()
        {
            _circleNumber = (int)Utilities.GetRandomNumberFloat(0, 180);
            targetPoint = new Vector3(Mathf.Sin(_circleNumber * _speed) * _radius, 0,
                Mathf.Cos(_circleNumber * _speed) * _radius);
            direction = new Vector3(x, 0, z).DirectionTo(targetPoint).Normalized();
        }

        private void PinPongMoving(float delta)
        {
            x += direction.x * delta * _speed;
            z += direction.z * delta * _speed;
            if ((Mathf.Abs(x) >= Mathf.Abs(targetPoint.x)) && (Mathf.Sign(x) == Mathf.Sign(direction.x)) ||
                (Mathf.Abs(z) >= Mathf.Abs(targetPoint.z) && (Mathf.Sign(z) == Mathf.Sign(direction.z))))
                ChangeDirection();
            Translation = new Vector3(x, 0, z);
        }
    }
}