using System.Dynamic;
using Global.Data;
using Godot;

namespace HippieFall.Tunnels
{

    public class PerforatedWallConfig : Config
    {
        [Export] public float RotationSpeed { get; set; } = 1;

        public PerforatedWallConfig(PerforatedWallConfig perforatedWall)
        {
            RotationSpeed = perforatedWall.RotationSpeed;
        }

        public PerforatedWallConfig()
        {
        }

    }
}