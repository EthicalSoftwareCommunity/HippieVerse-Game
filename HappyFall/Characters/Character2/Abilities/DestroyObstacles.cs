using System.Collections.Generic;
using System.Timers;
using Global.Data.CharacterSystem;
using Godot;
using HippieFall.Tunnels;
using Timer = System.Timers.Timer;

namespace HippieFall.Characters
{
    public class DestroyObstacles : ActiveAbility
    {
        private List<Obstacle> _obstacles;
        private Timer _timer;
        
        public override void _Ready()
        {
            _obstacles = new List<Obstacle>();
            SetBlockSignals(!IsBlockingSignals());
            _timer = new Timer(100);
            _timer.Elapsed += Destroy;
            _timer.AutoReset = false;
        }

        private void OnAreaEntered(Area area)
        {
            if(area.GetOwner<Obstacle>() is Obstacle obstacle)
                _obstacles.Add(obstacle);
        }
        
        public override void Activate()
        {
            SetBlockSignals(!IsBlockingSignals());
            _timer.Start();
        }

        private void Destroy(object sender, ElapsedEventArgs e)
        {
            foreach (var obstacle in _obstacles)
                obstacle.Destroy();
            SetBlockSignals(!IsBlockingSignals());
        }
    }
}