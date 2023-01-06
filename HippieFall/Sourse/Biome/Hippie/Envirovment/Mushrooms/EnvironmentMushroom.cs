using Godot;
using System;
using System.Timers;
using Global;
using HippieFall;
using HippieFall.Game;
using Timer = System.Timers.Timer;

namespace HippieFall.Biomes
{
    public class EnvironmentMushroom : Spatial
    {
        private Vector3 _rotationSpeed ;
        private float _fallSpeed;
        private bool IsFalling = false;
        private void OnAreaEntered(Area area)
        {
            if (area.GetOwnerOrNull<Player>() != null && IsFalling == false)
            {
                _fallSpeed = HippieFallUtilities.Game.Level.LevelConfig.Speed *
                             Mathf.Log(HippieFallUtilities.Game.Level.LevelConfig.Speed);
                Mathf.Clamp(_fallSpeed, 1, 100);
                _rotationSpeed = new Vector3(Utilities.GetRandomNumberFloat(0.1f, 4), Utilities.GetRandomNumberFloat(0.1f, 4),
                    Utilities.GetRandomNumberFloat(0.1f, 4));
                IsFalling = true;
                Timer timer = new Timer(10000);
                timer.Elapsed += DestroyPlane;
                timer.Start();
            }
        }
        private void DestroyPlane(object sender, ElapsedEventArgs e)
        {
            QueueFree();   
        }

        public override void _PhysicsProcess(float delta)
        {
            if (IsFalling)
            {
                GlobalTranslation = new Vector3(GlobalTranslation.x, (float)(GlobalTranslation.y - 0.1f*Mathf.Sqrt(_fallSpeed)/2 - Math.Abs(GlobalTranslation.y/100)), GlobalTranslation.z );
                Rotate(new Vector3(1,0,0),(float)(_rotationSpeed.x*delta*Math.Log10(_fallSpeed))/2);
                Rotate(new Vector3(0,1,0), (float)(_rotationSpeed.y*delta*Math.Log10(_fallSpeed))/2);
                Rotate(new Vector3(0,0,1), (float)(_rotationSpeed.z*delta*Math.Log10(_fallSpeed))/2);
            }
        }
    }
}

