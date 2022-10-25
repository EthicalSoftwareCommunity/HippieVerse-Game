using System;

using Godot;

namespace HippieFall.Tunnels
{
    public abstract class Obstacle : Spatial
    {
        public event Action<Obstacle> OnDestroy; 
        public enum ObstacleTypes
        {
            Obstacle,
            Controller
        }

        public ObstacleTypes ObstacleType = ObstacleTypes.Obstacle;

        public void Destroy()
        {
            OnDestroy?.Invoke(this);
            QueueFree();
        }
    }
}