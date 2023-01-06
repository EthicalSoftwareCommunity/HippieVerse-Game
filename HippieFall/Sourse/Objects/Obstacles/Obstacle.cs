using System;
using Global.Data;
using Godot;

namespace HippieFall.Tunnels
{
    public abstract class Obstacle : Spatial
    {
        [Export] public virtual Config Config { get; set; } = new Config();
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