using Global.Data;
using Godot;

namespace HippieFall.Game
{
    public class GameCameraConfig : Config
    {
        [Export] public float Far;
        [Export] public float Fov;
        [Export] public float Near;
        [Export] public float Size;
        [Export] public float MaxAngleX = 5;
        [Export] public float MaxAngleZ = 7.5f;
        [Export] public float MinDistanceKoef = 0.01f;
        [Export] public float MaxDistanceKoef = 0.5f;

        public GameCameraConfig()
        {
            
        }
        public GameCameraConfig(GameCameraConfig config)
        {
            Far = config.Far;
            Fov = config.Fov;
            Near = config.Near;
            Size = config.Size;
            MaxAngleX = config.MaxAngleX;
            MaxAngleZ = config.MaxAngleZ;
            MinDistanceKoef = config.MinDistanceKoef;
            MaxDistanceKoef = config.MaxDistanceKoef;
        }

        public GameCameraConfig(Camera camera)
        {
            Far = camera.Far;
            Fov = camera.Fov;
            Near = camera.Near;
            Size = camera.Size;
        }
    }
}