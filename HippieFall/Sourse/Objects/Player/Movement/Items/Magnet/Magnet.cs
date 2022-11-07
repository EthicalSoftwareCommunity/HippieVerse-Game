using Godot;
using System.Collections.Generic;
using Global.Data;
using Global.Data.EffectSystem;

using HippieFall.Collectables;

namespace HippieFall.Items
{
    public class Magnet : Item, IEffectable
    {
        private List<Collectable> _collectablesInRange;
        private Vector3 _directionTo;
        private float _force;
        private float _speedForceCorrection;

        public override void _Ready()
        {
            _collectablesInRange = new List<Collectable>();
            Visible = false;
            SetPhysicsProcess(false);
            SetBlockSignals(false);
        }

        public override void _PhysicsProcess(float delta)
        {
            foreach (var collectable in _collectablesInRange)
            {
                if (collectable != null)
                {
                    _directionTo = collectable.GlobalTranslation.DirectionTo(GlobalTranslation);
                    collectable.GlobalTranslation += delta * _directionTo * (_force+_speedForceCorrection/3);
                }
            }
        }
        private void OnAreaEntered(Area area)
        {
            if (area.Owner is Collectable collectable)
                _collectablesInRange.Add(collectable);
        }

        private void OnAreaExited(Area area)
        {
            if (area.Owner is Collectable collectable)
                _collectablesInRange.Remove(collectable);
        }

        public void ChangeConfigData(Config config)
        {
            MagnetConfig magnetConfig = ((ItemConfig)config).MagnetConfig;
            
            _force = magnetConfig.Force;
            _speedForceCorrection = magnetConfig.SpeedForceCorrection;
            Visible = magnetConfig.IsMagnetActivated;
            SetPhysicsProcess(magnetConfig.IsMagnetActivated);
            SetBlockSignals(magnetConfig.IsMagnetActivated);
        }
    }

}
