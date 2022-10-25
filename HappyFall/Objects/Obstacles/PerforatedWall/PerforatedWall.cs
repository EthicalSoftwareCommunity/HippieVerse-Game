using Godot;
using Global;

namespace HippieFall.Tunnels
{
    class PerforatedWall : Obstacle
    {
        public override void _Ready()
        {
            Translation += new Vector3(
                Utilities.GetRandomNumberFloat(-1, 1), 
                0, 
                Utilities.GetRandomNumberFloat(-1, 1));
        }
    }
}