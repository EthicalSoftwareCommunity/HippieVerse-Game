using Global;
using Godot;
using Timer = System.Timers.Timer;

namespace HippieFall.Tunnels
{
    internal class ChangePositionPlatform : Obstacle
    {
        [Export] private Spatial _center;
        [Export] private float _radius;
        private float _spawnPointX;
        private float _spawnPointZ;
        private Timer _timer;

        public override void _Ready()
        {
            _center = GetNode<Spatial>("Center");
            SpawnPlatform(false);
        }

        private void SpawnPlatform(bool offset)
        {
            if (offset)
            {
                _spawnPointX = _radius;
                while (Mathf.Abs(_spawnPointX) >= _radius)
                {
                    _spawnPointX = Utilities.GetRandomNumberFloat(-_radius, _radius);
                    _spawnPointX = Mathf.Clamp(_spawnPointX, -1 * _radius, _radius);
                }
            }
            else _spawnPointX = Utilities.GetRandomNumberFloat(-_radius, _radius);

            int verticalPositionCoefficient = Utilities.GetRandomNumberInt(-1, 1,
                Utilities.Parameter.WITH_OUT_ZERO);

            _spawnPointZ = Mathf.Sqrt(_radius * _radius - _spawnPointX * _spawnPointX) * verticalPositionCoefficient;
            Translation = new Vector3(_spawnPointX, 0, _spawnPointZ);
            RotateY(Mathf.Deg2Rad(-90));
        }
    }
}