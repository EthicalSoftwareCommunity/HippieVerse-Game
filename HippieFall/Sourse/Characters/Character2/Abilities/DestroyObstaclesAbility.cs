using System.Collections.Generic;
using Global.Data.CharacterSystem;
using Godot;
using HippieFall.Tunnels;
using Timer = System.Timers.Timer;

namespace HippieFall.Characters
{
    public class DestroyObstaclesAbility : ActiveAbility
    {
        private List<Obstacle> _obstacles;

        public override void _Ready()
        {
            _obstacles = new List<Obstacle>();
        }

        private void OnAreaEntered(Area area)
        {
            if(area.GetOwnerOrNull<Obstacle>() is Obstacle obstacle)
                _obstacles.Add(obstacle);
        }

        private void OnAreaExited(Area area)
        {
            if(area.GetOwnerOrNull<Obstacle>() is Obstacle obstacle)
                _obstacles.Remove(obstacle);
        }
        
        public override void Activate()
        { 
            DestroyEnteredObstacles();
        }

        private void DestroyEnteredObstacles()
        {
            List<Obstacle> obstacles = new List<Obstacle>();
            obstacles.AddRange(_obstacles);
            foreach (Obstacle obstacle in obstacles)
            {
                obstacle?.Destroy();
            }
            _obstacles.Clear();
        }
    }
}