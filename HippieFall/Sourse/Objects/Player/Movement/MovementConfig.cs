using Global.Data;

namespace HippieFall
{
	public class MovementConfig : Config
	{
		public float Speed { get; private set; } = 10f;

		public MovementConfig()
		{
		}
		public MovementConfig(MovementConfig movementConfig)
		{
			Speed = movementConfig.Speed;
		}
	}
}
