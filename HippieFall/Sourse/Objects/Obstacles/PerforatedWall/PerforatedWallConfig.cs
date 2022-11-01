using System.Dynamic;
using Global.Data;

namespace HippieFall.Tunnels
{

    public class PerforatedWallConfig : Config
    {
        public float RotationSpeed { get; set; } = 1;

        public PerforatedWallConfig(PerforatedWallConfig perforatedWall)
        {
            RotationSpeed = perforatedWall.RotationSpeed;
        }

        public PerforatedWallConfig()
        {
        }

    }
}