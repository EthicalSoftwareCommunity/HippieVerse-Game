using Global.Data;

namespace HippieFall
{
	public class MovementConfig : Config
	{
		public float Speed { get; set; } = 4f;
		public float Radius { get; set; } = 2.6f;

		public MovementConfig()
		{
		}
		public MovementConfig(MovementConfig movementConfig)
		{
			Speed = movementConfig.Speed;
			Radius = movementConfig.Radius;
		}
	}
}
