using Global.Data;
using Godot;

namespace HippieFall.Game
{
    public class GameCameraConfig : Config
    {
        public float Far;
        public float Fov;
        public float Near;
        public float Size;
        public float MaxAngleX = 5;
        public float MaxAngleZ = 7.5f;
        public float MinDistanceKoef = 0.01f;
        public float MaxDistanceKoef = 0.5f;
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