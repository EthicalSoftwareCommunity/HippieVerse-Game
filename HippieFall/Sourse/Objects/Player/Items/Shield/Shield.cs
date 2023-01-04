using Godot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Global.Data;
using Global.Data.EffectSystem;
using HippieFall.Tunnels;

namespace HippieFall.Items
{
    public class Shield : Item, IEffectable
    {
        private List<Obstacle> _obstacles;
        private ShieldConfig _config;
        
        public override void _Ready()
        {
            _config = new ShieldConfig();
            _obstacles = new List<Obstacle>();
            Visible = _config.IsShieldActivated;
            SetBlockSignals(!_config.IsShieldActivated);
        }

        private void OnShieldAreaEntered(Area area)
        {
            if (area.GetOwnerOrNull<Obstacle>() is Obstacle obstacle)
            {
                if(_config.IsShieldActivated)
                    DestroyEnteredObstacles();
            }
        }
   
        private void OnDestroyAreaEntered(Area area)
        {
            if(area.GetOwnerOrNull<Obstacle>() is Obstacle obstacle)
                _obstacles.Add(obstacle);
        }

        private void OnDestroyAreaExited(Area area)
        {
            if(area.GetOwnerOrNull<Obstacle>() is Obstacle obstacle)
                _obstacles.Remove(obstacle);
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

        public void ChangeConfigData(Config config)
        {
            _config = new(((ItemConfig)config).ShieldConfig);
            Visible = _config.IsShieldActivated;
            SetBlockSignals(!_config.IsShieldActivated);
        }
    }
}

